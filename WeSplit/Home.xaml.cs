using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }
        public Trips FinishTrips { get; set; } = null;
        ObservableCollection<Trips> _data;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}AllTrips.txt";
            var lines = File.ReadAllLines(database);
            int count = int.Parse(lines[0]);
            _data = new ObservableCollection<Trips>();
            for (int i = 0; i < count; i++)
            {
                var line1 = lines[i * 6 + 1];
                var line2 = lines[i * 6 + 2];
                var line3 = lines[i * 6 + 3];
                var line4 = lines[i * 6 + 4];
                var line5 = lines[i * 6 + 5];
                var line6 = lines[i * 6 + 6];

                var trips = new Trips()
                {
                    Name = line1,
                    Introduce = line2,
                    Picture = line3,
                    Icon = line4,
                    ThanhVien = line5,
                    DiaDiem = line6
                };
                if (trips.Icon == "None")
                {
                    save.Add(i);
                    _data.Add(trips);
                }
            }

            if (_data.Count > 3)
            {
                this.Bot.Visibility = Visibility.Visible;
            }

            info.CurrentPage = 1;
            info.RowsPerPage = 3;
            info.Count = _data.Count;
            info.TotalPages = (info.Count / info.RowsPerPage) +
                (info.Count % info.RowsPerPage == 0 ? 0 : 1);

            Thread thread = new Thread(delegate ()
            {
                Dispatcher.Invoke(() =>
                {
                    dataListview.ItemsSource = _data.Take(info.RowsPerPage);
                });
            });

            thread.Start();
        }

        PagingInfo info = new PagingInfo();
        class PagingInfo : INotifyPropertyChanged
        {
            public int TotalPages { get; set; }

            private int _currentPage = 0;
            public int CurrentPage
            {
                get => _currentPage;
                set
                {
                    _currentPage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
                }
            }
            private int _page1 = 1;
            public int Page1
            {
                get => _page1;
                set
                {
                    _page1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Page1"));
                }
            }
            private int _page2 = 2;
            public int Page2
            {
                get => _page2;
                set
                {
                    _page2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Page2"));
                }
            }
            private int _page3 = 3;
            public int Page3
            {
                get => _page3;
                set
                {
                    _page3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Page3"));
                }
            }

            public int Count { get; set; }
            public int RowsPerPage { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        private void Page1_Click(object sender, RoutedEventArgs e)
        {
            info.CurrentPage = 1;
            dataListview.ItemsSource = _data.Take(info.RowsPerPage);
        }
        private void Page2_Click(object sender, RoutedEventArgs e)
        {
            info.CurrentPage = 2;
            dataListview.ItemsSource = _data.Skip((info.CurrentPage - 1) * info.RowsPerPage).Take(info.RowsPerPage);
        }
        private void Page3_Click(object sender, RoutedEventArgs e)
        {
            info.CurrentPage = 3;
            dataListview.ItemsSource = _data.Skip((info.CurrentPage - 1) * info.RowsPerPage).Take(info.RowsPerPage);
        }
        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (info.CurrentPage <= info.TotalPages)
            {
                info.CurrentPage--;
                dataListview.ItemsSource =
                _data
                    .Skip((info.CurrentPage - 1) * info.RowsPerPage)
                    .Take(info.RowsPerPage);
                if (info.CurrentPage <= 1)
                {
                    info.CurrentPage = 1;
                }
            }
        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (info.CurrentPage < info.TotalPages)
            {
                info.CurrentPage++;
                dataListview.ItemsSource =
                _data
                    .Skip((info.CurrentPage - 1) * info.RowsPerPage)
                    .Take(info.RowsPerPage);
            }
        }

        private void dataListview_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem as Trips;
            int index = dataListview.Items.IndexOf(item) + ((info.CurrentPage - 1) * info.RowsPerPage);
            if (item != null)
            {
                USHome.Children.Clear();
                USHome.Children.Add(new TripsDetail(item));
            }
        }

        private void responsivetrip_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
        public List<int> save = new List<int>();
        private void MapCheck_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = dataListview.Items.IndexOf(item) + ((info.CurrentPage - 1) * info.RowsPerPage);
            if (_data[index].Icon == "MapCheck")
            {
                _data[index].Icon = "None";
                var folder = AppDomain.CurrentDomain.BaseDirectory;
                var database = $"{folder}AllTrips.txt";
                var lines = File.ReadAllLines(database);
                lines[save[index] * 6 + 4] = "None";
                File.WriteAllLines(database, lines);
                //Xu ly xoa
                if (index == -1)
                {

                }
                else
                {
                    var db = $"{folder}Finish.txt";
                    List<string> resline = File.ReadAllLines(db).ToList();
                    var line = File.ReadAllLines(db);
                    int count = int.Parse(line[0]);
                    if (count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (resline[i * 6 + 1] == _data[index].Name)
                            {
                                count--;
                                resline[0] = (count--).ToString();
                                resline.RemoveAt(i * 6 + 6);
                                resline.RemoveAt(i * 6 + 5);
                                resline.RemoveAt(i * 6 + 4);
                                resline.RemoveAt(i * 6 + 3);
                                resline.RemoveAt(i * 6 + 2);
                                resline.RemoveAt(i * 6 + 1);
                                File.WriteAllLines(db, resline.ToArray());
                            }
                        }
                    }
                }
            }
            else
            {
                _data[index].Icon = "MapCheck";
                FinishTrips = _data[index];
                var folder = AppDomain.CurrentDomain.BaseDirectory;
                var db = $"{folder}AllTrips.txt";
                var lines = File.ReadAllLines(db);
                lines[save[index] * 6 + 4] = "MapCheck";
                File.WriteAllLines(db, lines);
                var database = $"{folder}Finish.txt";
                var flines = File.ReadAllLines(database);
                int count = int.Parse(flines[0]);
                count++;
                flines[0] = (count++).ToString();
                File.WriteAllLines(database, flines);
                using (StreamWriter sw = File.AppendText(database))
                {
                    sw.WriteLine(FinishTrips.Name);
                    sw.WriteLine(FinishTrips.Introduce);
                    sw.WriteLine(FinishTrips.Picture);
                    sw.WriteLine(FinishTrips.Icon);
                    sw.WriteLine(FinishTrips.ThanhVien);
                    sw.WriteLine(FinishTrips.DiaDiem);
                }
            }
        }

        private void imgSearch_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Nếu ô tìm kiếm rỗng, thì lấy tất cả sản phẩm
            if (SearchTexbox.Text.Length == 0)
            {
                dataListview.ItemsSource = _data.Take(info.RowsPerPage);
                this.Bot.Visibility = Visibility.Visible;
            }
            // Nếu ô tìm kiếm có nội dung   
            else
            {
                // Tạo mới danh sách sản phẩm có tên chứa nội dung ô tìm kiếm
                ObservableCollection<Trips> searchTrips = new ObservableCollection<Trips>();
                for (int i = 0; i < _data.Count; i++)
                {
                    if ((_data[i].Name).ToLower().Contains((SearchTexbox.Text).ToLower()) || (_data[i].Name).ToUpper().Contains((SearchTexbox.Text).ToUpper()) || (_data[i].ThanhVien).ToLower().Contains((SearchTexbox.Text).ToLower()) || (_data[i].ThanhVien).ToUpper().Contains((SearchTexbox.Text).ToUpper()) || (_data[i].DiaDiem).ToLower().Contains((SearchTexbox.Text).ToLower()) || (_data[i].DiaDiem).ToUpper().Contains((SearchTexbox.Text).ToUpper())) // Nếu tìm thấy tên phù hợp
                    {
                        searchTrips.Add(_data[i]); // Thì thêm vào danh sách mới
                    }
                }

                // Nếu tìm thấy ít nhất 1 sản phẩm thì hiển thị, không thì thông báo
                if (searchTrips.Count > 0 && searchTrips.Count <= 3)
                {
                    this.Bot.Visibility = Visibility.Collapsed;
                    dataListview.ItemsSource = searchTrips.Take(searchTrips.Count);
                }
                else if (searchTrips.Count > 3)
                {
                    this.Bot.Visibility = Visibility.Collapsed;
                    dataListview.ItemsSource = searchTrips.Take(searchTrips.Count);
                }
                else
                {
                    MessageBox.Show("Not found");
                }

                // Làm trống ô tìm kiếm
                SearchTexbox.Text = "";
            }
        }
    }
}
