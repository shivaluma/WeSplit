using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Interaction logic for TripsDetail.xaml
    /// </summary>
    public partial class TripsDetail : UserControl
    {
        public Trips _data;
        BindingList<Trips> _list;
        string nameTrip;
        public TripsDetail(Trips t)
        {
            InitializeComponent();
            _data = t;
            nameTrip = _data.Name;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            this.DataContext = _data;
            if (_data.Icon == "MapCheck")
            {
                _update.Visibility = Visibility.Collapsed;
            }
            _list = new BindingList<Trips>();
            Itemscontrol.ItemsSource = _list;
            var appStartPath = AppDomain.CurrentDomain.BaseDirectory;
            appStartPath = appStartPath + $"\\ListTrips\\{nameTrip}\\";
            var t = new Trips()
            {
                Description = "",
                Expenses = new ObservableCollection<string>(),
                Routes = new ObservableCollection<string>(),
                Members = new ObservableCollection<string>(),
                Money = new ObservableCollection<string>(),
                Remark = new ObservableCollection<string>(),
                Images = new BindingList<string>()
            };

            //Images
            string fileImages = appStartPath + $"Images.txt";
            var readImages = File.ReadAllLines(fileImages);

            for (int i = 0; i < readImages.Length; i++)
            {
                t.Images.Add(appStartPath + readImages[i]);
            }

            //Routes
            string fileRoutes = appStartPath + $"Routes.txt";
            var readRoutes = File.ReadAllLines(fileRoutes);
            //t.Routes.Add(readcotmoctest.Replace("//", "\r\n"));
            for (int i = 0; i < readRoutes.Length; i++)
            {
                t.Routes.Add(readRoutes[i]);
            }

            //Members
            string fileMembers = appStartPath + $"Members.txt";
            var readMembers = File.ReadAllLines(fileMembers);
            string memleader = readMembers[0] + " (Leader): " + readMembers[1] + " VNĐ";
            t.Members.Add(memleader);
            for (int i = 2; i < readMembers.Length; i = i + 2)
            {
                string mem = readMembers[i] + ": " + readMembers[i + 1] + " VNĐ";
                t.Members.Add(mem);
            }

            //Expenses
            string fileExpenses = appStartPath + $"Expenses.txt";
            var readExpenses = File.ReadAllLines(fileExpenses);
            for (int i = 0; i < readExpenses.Length; i = i + 2)
            {
                string ex = readExpenses[i] + ": " + readExpenses[i + 1] + " VNĐ";
                t.Expenses.Add(ex);
            }

            _list.Add(t);
        }

        private void _update_MouseUp(object sender, MouseButtonEventArgs e)
        {
            USTripsDetail.Children.Clear();
            USTripsDetail.Children.Add(new UpdateTrips(_data));
        }

        private void _piechart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            USTripsDetail.Children.Clear();
            USTripsDetail.Children.Add(new USChart(_data));
        }

        private void _split_MouseUp(object sender, MouseButtonEventArgs e)
        {
            USTripsDetail.Children.Clear();
            USTripsDetail.Children.Add(new SplitMoney(_data));
        }
    }
}
