using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for UpdateTrips.xaml
    /// </summary>
    public partial class UpdateTrips : UserControl
    {
        public Trips _data;
        BindingList<Trips> _list;
        string nameTrip;
        public UpdateTrips(Trips t)
        {
            InitializeComponent();
            _data = t;
            nameTrip = _data.Name;
        }

        ObservableCollection<string> Members = new ObservableCollection<string>();
        ObservableCollection<string> Expenses = new ObservableCollection<string>();
        List<string> arr = new List<string>() {
            "Members",
            "Expenses",
            "Routes",
            "Images",
            "Leader"
        };
        private void imgCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            USUpdateTrips.Children.Clear();
            USUpdateTrips.Children.Add(new TripsDetail(_data));
        }
        private void Copy(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
                File.Copy(file, System.IO.Path.Combine(targetDir, System.IO.Path.GetFileName(file)));

            foreach (var directory in Directory.GetDirectories(sourceDir))
                Copy(directory, System.IO.Path.Combine(targetDir, System.IO.Path.GetFileName(directory)));
        }
        private void imgSave_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (tripName.Text.Trim() != "" && imgTrip.ImageSource != null && Introduce.Text.Trim() != "")
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save", "", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    // Sửa dữ liệu file AllTrips
                    var folder = AppDomain.CurrentDomain.BaseDirectory;
                    var database = $"{folder}AllTrips.txt";
                    var avatar = $"{folder}Images";
                    var lines = File.ReadAllLines(database);
                    string imgtripr = System.IO.Path.GetFileName(imgTrip.ImageSource.ToString());
                    if (_data.Picture != imgtripr)
                    {
                        var imgAv = ((BitmapImage)imgTrip.ImageSource).UriSource.ToString().Remove(0, 8);
                        var appStartPath111 = String.Format(avatar + "\\" + imgtripr);
                        string imgAvName = System.IO.Path.GetFileName(imgAv.ToString());
                        string appStartPath111Name = System.IO.Path.GetFileName(appStartPath111.ToString());
                        if (!File.Exists(avatar + "\\" + imgAvName))
                        {
                            File.Copy(imgAv, appStartPath111, true);
                        }    
                    }
                    for (int i = 1; i < lines.Length; i += 6)
                    {
                        string linesMembers = "";
                        if (lines[i] == nameTrip)
                        {
                            lines[i] = tripName.Text;
                            lines[i + 1] = Introduce.Text;
                            lines[i + 2] = imgtripr;
                            for (int j = 0; j <= Members.Count() - 1; j += 2)
                            {
                                if (j + 1 == Members.Count() - 1)
                                {
                                    linesMembers = String.Concat(linesMembers, Members[j]);
                                }
                                else
                                    linesMembers = String.Concat(linesMembers, Members[j] + " - ");
                            }
                            lines[i + 4] = linesMembers;
                            lines[i + 5] = Routes.Text.ToString().Replace("\r", "").Replace("\n", "\\\\");
                        }
                    }
                    File.WriteAllLines(database, lines);

                    String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                    appStartPath = appStartPath + $"\\ListTrips";
                    string path2 = System.IO.Path.Combine(appStartPath, nameTrip);
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
                                {
                                    sw1.Write(Routes.Text);
                                }
                                break;
                            case 4:
                                using (StreamWriter sw1 = File.CreateText(pathDetail))
                                {
                                    foreach (string nameImg in _list[0].Images)
                                    {
                                        string name = System.IO.Path.GetFileName(nameImg);
                                        if (File.Exists(path2 + "\\" + name))
                                        {
                                            sw1.WriteLine(name);
                                        }
                                        else
                                        {
                                            sw1.WriteLine(name);
                                            appStartPath = String.Format(path2 + "\\" + name);
                                            File.Copy(nameImg, appStartPath, true);
                                        }
                                    }
                                    //Xoa anh
                                    foreach (string item in imagesExsis)
                                    {
                                        try
                                        {
                                            File.Delete(item);
                                        }
                                        catch (IOException)
                                        {
                                            //file is currently locked
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    //Đổi tên folder
                    var source = $"{folder}" + $"ListTrips\\{nameTrip}\\";
                    var des = $"{folder}" + $"ListTrips\\{tripName.Text}\\";
                    if (nameTrip == tripName.Text)
                    {
                        _data.Introduce = Introduce.Text;
                        _data.Picture = imgtripr;
                        USUpdateTrips.Children.Clear();
                        USUpdateTrips.Children.Add(new TripsDetail(_data));
                    }
                    else
                    {
                        _data.Name = tripName.Text;
                        _data.Introduce = Introduce.Text;
                        _data.Picture = imgtripr;
                        if (!Directory.Exists(des))
                        {
                            Directory.CreateDirectory(des);
                            Copy(source, des);
                            //Directory.Move(source, des);     
                        }
                        USUpdateTrips.Children.Clear();
                        USUpdateTrips.Children.Add(new TripsDetail(_data));
                    }
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
                _list[0].Expenses.Add(ko);
                listExpenditures.ItemsSource = _list;
                Expenses.Add(expenses);
                Expenses.Add(pr.ToString());
                Expenditures.Text = "";
                Prices.Text = "";
            }
        }

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

                string koko = mem + ": " + pr.ToString() + " VNĐ";
                _list[0].Members.Add(koko);
                listMembers.ItemsSource = _list;
                Members.Add(mem.Trim());
                Members.Add(pr.ToString());
                memberName.Text = "";
                moneyPaid.Text = "";
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = _data;
            tripName.Text = _data.Name;
            Introduce.Text = _data.Introduce;
            Trips ttt = new Trips()
            {
                Picture = _data.Picture
            };
            _list = new BindingList<Trips>();
            listExpenditures.ItemsSource = _list;
            listMembers.ItemsSource = _list;
            listImagessss.ItemsSource = _list;
            String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            appStartPath = appStartPath + $"\\ListTrips\\{nameTrip}\\";
            var t = new Trips()
            {
                Expenses = new ObservableCollection<string>(),
                Routes = new ObservableCollection<string>(),
                Members = new ObservableCollection<string>(),
                Images = new BindingList<string>()
            };

            //Images
            string fileImages = appStartPath + $"Images.txt";
            var readImages = File.ReadAllLines(fileImages);

            for (int i = 0; i < readImages.Length; i++)
            {
                t.Images.Add(appStartPath + readImages[i]);
                listImages.Add(readImages[i]);
            }

            //Routes
            string fileRoutes = appStartPath + $"Routes.txt";
            var readRoutes = File.ReadAllText(fileRoutes);
            Routes.Text = readRoutes;

            //Members
            string fileMembers = appStartPath + $"Members.txt";
            var readMembers = File.ReadAllLines(fileMembers);
            t.Leader = readMembers[0].ToString() + " (Leader): " + readMembers[1].ToString() + " VNĐ";
            t.Members.Add(t.Leader);
            Members.Add(readMembers[0]);
            Members.Add(readMembers[1]);
            for (int i = 2; i < readMembers.Length; i = i + 2)
            {              
                string mem = readMembers[i] + ": " + readMembers[i + 1] + " VNĐ";
                t.Members.Add(mem);
                Members.Add(readMembers[i]);
                Members.Add(readMembers[i + 1]);
            }

            //Expenses
            string fileExpenses = appStartPath + $"Expenses.txt";
            var readExpenses = File.ReadAllLines(fileExpenses);
            for (int i = 0; i < readExpenses.Length; i = i + 2)
            {
                string ex = readExpenses[i] + " : " + readExpenses[i + 1] + " VNĐ";
                t.Expenses.Add(ex);
                Expenses.Add(readExpenses[i]);
                Expenses.Add(readExpenses[i + 1]);
            }
            _list.Add(t);
        }

        ObservableCollection<string> listImages = new ObservableCollection<string>();
        private void Imgsss_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();
            if (result == true)
            {
                foreach (string item in open.FileNames)
                {
                    listImages.Add(item);
                    _list[0].Images.Add(item);
                }
            }
        }

        private void buttonEditExpenses_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonDeleteExpenses_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = _list[0].Expenses.IndexOf(item.ToString());
            _list[0].Expenses.RemoveAt(index);
            Expenses.RemoveAt(index * 2 + 1);
            Expenses.RemoveAt(index * 2);
        }

        private void buttonEditMembers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonDeleteMembers_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = _list[0].Members.IndexOf(item.ToString());
            if (index == 0)
            {
                MessageBox.Show("Đây là leader, không thể xoá!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _list[0].Members.RemoveAt(index);
                Members.RemoveAt(index * 2 + 1);
                Members.RemoveAt(index * 2);
            }
        }
        List<string> imagesExsis = new List<string>();
        private void buttonDeleteImages_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            imagesExsis.Add(item.ToString());
            int index = _list[0].Images.IndexOf(item.ToString());
            _list[0].Images.RemoveAt(index);
            listImages.RemoveAt(index);
        }
    }
}
