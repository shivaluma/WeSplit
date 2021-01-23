using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for NewTrips.xaml
    /// </summary>
    public partial class NewTrips : Window
    {
        public NewTrips()
        {
            InitializeComponent();
        }

        ObservableCollection<string> Members = new ObservableCollection<string>();
        ObservableCollection<string> Expenses = new ObservableCollection<string>();
        List<string> arr = new List<string>() {
            "Members",
            "Expenses",
            "Routes",
            "Images"
        };

        private void imgCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void imgSave_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (tripName.Text.Trim() != "" && imgTrip.ImageSource != null && Introduce.Text.Trim() != "")
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save", "", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    var folder = AppDomain.CurrentDomain.BaseDirectory;
                    var avatar = $"{folder}Images";
                    var database = $"{folder}AllTrips.txt";
                    var lines = File.ReadAllLines(database);
                    int count = int.Parse(lines[0]);
                    count++;
                    lines[0] = (count++).ToString();
                    File.WriteAllLines(database, lines);
                    using (StreamWriter sw = File.AppendText(database))
                    {
                        sw.WriteLine(tripName.Text);
                        sw.WriteLine(Introduce.Text);
                        string _imgtrip = System.IO.Path.GetFileName(imgTrip.ImageSource.ToString());
                        sw.WriteLine(_imgtrip);
                        sw.WriteLine("None");
                        int lastIndex = Members.Count - 1;
                        for (int index = 0; index <= lastIndex; index++)
                        {
                            if (index == lastIndex)
                            {
                                //this is the last item
                                sw.WriteLine(Members[index]);
                            }
                            else sw.Write(Members[index] + " - ");
                        }
                        sw.WriteLine("0");
                    }
                    string imgtripr = System.IO.Path.GetFileName(imgTrip.ImageSource.ToString());
                    var imgAv = ((BitmapImage)imgTrip.ImageSource).UriSource.ToString().Remove(0, 8);
                    var appStartPath111 = String.Format(avatar + "\\" + imgtripr);
                    File.Copy(imgAv, appStartPath111, true);



                    String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                    appStartPath = appStartPath + "\\ListTrips";
                    string path2 = System.IO.Path.Combine(appStartPath, tripName.Text);
                    if (!Directory.Exists(path2))
                    {
                        Directory.CreateDirectory(path2);
                        string path3 = path2 + $"\\{tripName.Text}.txt";
                        if (File.Exists(path3))
                        {
                            File.Delete(path3);
                        }
                        using (StreamWriter sw = File.CreateText(path3))
                        {
                            string imgtrip = System.IO.Path.GetFileName(imgTrip.ImageSource.ToString());
                            var imgdes2 = ((BitmapImage)imgTrip.ImageSource).UriSource.ToString().Remove(0, 8);
                            sw.WriteLine(tripName.Text);
                            sw.WriteLine(Introduce.Text);
                            sw.WriteLine(imgtrip);
                            appStartPath = String.Format(path2 + "\\" + imgtrip);
                            File.Copy(imgdes2, appStartPath, true);
                            for (int i = 0; i < 4; i++)
                            {
                                string pathDetail = path2 + $"\\{arr[i]}.txt";
                                switch (i + 1)
                                {
                                    case 1:
                                        using (StreamWriter sw1 = File.CreateText(pathDetail))
                                        {
                                            foreach (string content in Members)
                                            {
                                                sw1.WriteLine(content);
                                            }
                                        }
                                        break;
                                    case 2:
                                        using (StreamWriter sw1 = File.CreateText(pathDetail))
                                        {
                                            foreach (string content in Expenses)
                                            {
                                                sw1.WriteLine(content);
                                            }
                                        }
                                        break;
                                    case 3:
                                        using (StreamWriter sw1 = File.CreateText(pathDetail))
                                            break;
                                    case 4:
                                        using (StreamWriter sw1 = File.CreateText(pathDetail))
                                            break;
                                }
                            }
                        }
                    }
                    MainWindow m = new MainWindow();
                    m.Show();
                    this.Close();
                }
            }
            else
                MessageBox.Show("You did not enter the name, introduce or image trip!!!");
        }

        private void ChooseImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();
            if (result == true)
            {
                var img = open.FileNames;
                ImageSource imgsource = new BitmapImage(new Uri(img[0].ToString()));
                imgTrip.ImageSource = imgsource;
            }
        }

        private void imgAddPays_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Expenditures.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập tên khoản chi!!", "", MessageBoxButton.OK);
            }
            else
            {
                string expenses = Expenditures.Text;
                string prices = Prices.Text;
                ulong pr;
                if (prices == "")
                {
                    pr = 0;
                }
                else
                    pr = Convert.ToUInt64(prices);

                string ko = expenses + " : " + pr.ToString() + " VNĐ";
                TextBlock o = new TextBlock();
                o.Text = ko;
                listExpenditures.Items.Add(o);
                Expenses.Add(expenses);
                Expenses.Add(pr.ToString());
                Expenditures.Text = "";
                Prices.Text = "";
            }
        }
        int flag = 0;
        private void imgAddMember_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (memberName.Text.Trim() != "")
            {
                string mem = memberName.Text;
                // advance payment
                string money = moneyPaid.Text;
                ulong pr;
                if (money == "")
                {
                    pr = 0;
                }
                else
                    pr = Convert.ToUInt64(money);

                // nameOfMember
                TextBlock o = new TextBlock();
                string koko = mem + ": " + pr.ToString() + " VNĐ";
                o.Text = koko;
                if (flag == 0)
                {
                    string ooo = mem + " (Leader): " + pr.ToString() + " VNĐ";
                    o.Text = ooo;
                    listMembers.Items.Add(o);
                    Members.Add(mem.Trim());
                    Members.Add(pr.ToString());
                    memberName.Text = "";
                    moneyPaid.Text = "";
                    flag++;
                }    
                else
                {
                    listMembers.Items.Add(o);
                    Members.Add(mem.Trim());
                    Members.Add(pr.ToString());
                    memberName.Text = "";
                    moneyPaid.Text = "";
                } 
            }
            else
                MessageBox.Show("Chưa nhập tên thành viên!!", "", MessageBoxButton.OK);
        }

        private void Prices_TextChanged(object sender, TextChangedEventArgs e)
        {
            Prices.Text = Regex.Replace(Prices.Text, "[^0-9]+", "");
        }

        private void moneyPaid_TextChanged(object sender, TextChangedEventArgs e)
        {
            moneyPaid.Text = Regex.Replace(moneyPaid.Text, "[^0-9]+", "");
        }
    }
}
