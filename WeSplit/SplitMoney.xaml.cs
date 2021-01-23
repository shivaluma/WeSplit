using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for SplitMoney.xaml
    /// </summary>
    public partial class SplitMoney : UserControl
    {
        public Trips _data;
        BindingList<Trips> _list;
        string nameTrip;
        long Sum = 0;
        int CountMember = 0;
        public SplitMoney(Trips t)
        {
            InitializeComponent();
            _data = t;
            nameTrip = _data.Name;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _list = new BindingList<Trips>();
            Itemscontrol.ItemsSource = _list;
            var appStartPath = AppDomain.CurrentDomain.BaseDirectory;
            appStartPath = appStartPath + $"\\ListTrips\\{nameTrip}\\";
            var t = new Trips()
            {
                Expenses = new ObservableCollection<string>(),
                Members = new ObservableCollection<string>(),
                Money = new ObservableCollection<string>(),
                Remark = new ObservableCollection<string>(),
                Leader = ""
            };

            //Members
            string fileMembers = appStartPath + $"Members.txt";
            var readMembers = File.ReadAllLines(fileMembers);
            t.Members.Add(readMembers[0] + " (Leader)");
            for (int i = 2; i < readMembers.Length; i = i + 2)
            {
                t.Members.Add(readMembers[i]);
            }

            for (int i = 0; i < readMembers.Length; i++)
            {
                if (i % 2 != 0)
                {
                    t.Money.Add(readMembers[i] + " VNĐ");
                    CountMember++;
                }
            }

            //Expenses
            string fileExpenses = appStartPath + $"Expenses.txt";
            var readExpenses = File.ReadAllLines(fileExpenses);
            for (int i = 0; i < readExpenses.Length; i = i + 2)
            {
                string ex = readExpenses[i] + ": " + readExpenses[i + 1] + " VNĐ";
                t.Expenses.Add(ex);
            }
            for (int i = 0; i < readExpenses.Length; i++)
            {
                if (i % 2 != 0)
                    Sum += long.Parse(readExpenses[i]) * CountMember;
            }

            //Remark
            Sum = Sum / CountMember;
            long al = long.Parse(readMembers[1]);
            if (al < Sum)
                t.Remark.Add($"Cần phải đóng thêm {Math.Abs(Sum - al)} VNĐ");
            else if (al > Sum)
                t.Remark.Add($"Tiền dư {al - Sum} VNĐ");
            else
                t.Remark.Add($"Đã đóng đủ");
            for (int i = 3; i < readMembers.Length; i += 2)
            {
                long a = long.Parse(readMembers[i]);
                if (a < Sum)
                    t.Remark.Add($"Cần phải đóng thêm {Math.Abs(Sum - a)} VNĐ cho {readMembers[0]}");
                else if (a > Sum)
                    t.Remark.Add($"Được {readMembers[0]} trả lại {a - Sum} VNĐ");
                else
                    t.Remark.Add($"Đã đóng đủ");
            }
            _list.Add(t);
        }

        private void imgCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            USSplitMoney.Children.Clear();
            USSplitMoney.Children.Add(new TripsDetail(_data));
        }
    }
}
