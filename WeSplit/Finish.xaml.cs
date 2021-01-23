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
    /// Interaction logic for Finish.xaml
    /// </summary>
    public partial class Finish : UserControl
    {
        public Finish()
        {
            InitializeComponent();
        }
        public Trips FinishTrips { get; set; } = null;
        ObservableCollection<Trips> _data;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}Finish.txt";
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
                _data.Add(trips);
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
                // Cập nhật UI
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
                USFinish.Children.Clear();
                USFinish.Children.Add(new TripsDetail(item));
            }
        }

       

        private void responsivetrip_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //string a = Trip.Width.ToString();
            //if (Trip.Width.ToString().Equals(a))
            //{
            //    for (int i = 0; i < _data.Count; i++)
            //    {
            //        _data[i].Www = "356";
            //    }
            //}
            //else
            //{
            //    for (int j = 0; j < _data.Count; j++)
            //    {
            //        _data[j].Www = "416";
            //    }
            //}
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
