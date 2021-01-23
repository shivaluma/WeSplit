using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        private Random _rng = new Random();
        static List<string> Img = new List<string>();
        static List<string> Desc = new List<string>();
        ObservableCollection<Trips> _data;
        DispatcherTimer dT = new DispatcherTimer();
        string dataFile = "";
        public SplashScreen()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            dataFile = $"{folder}Check.txt";
            var data = File.ReadAllText(dataFile);
            if (data == "true")
            {
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
            }
            else
            {
                dT.Tick += new EventHandler(dt_Tick);
                dT.Interval = new TimeSpan(0, 0, 60);
                dT.Start();
            }
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            if (flag == true)
            {
                MainWindow m = new MainWindow();
                m.Show();
                dT.Stop();
                this.Close();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            dataFile = $"{folder}Check.txt";
            File.AppendAllText(dataFile, "true");
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            dataFile = $"{folder}Check.txt";
            File.Create(dataFile).Close();
        }

        bool flag = true;
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
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

                var trips = new Trips()
                {
                    Name = line1,
                    Introduce = line2,
                    Picture = line3,
                };

                _data.Add(trips);
            }
            var k = _rng.Next(_data.Count);
            Title.Text = _data[k].Name;
            Description.Text = _data[k].Introduce;
            dataFile = $"{folder}Images\\{_data[k].Picture}";
            BackgoundImg.ImageSource = new BitmapImage(new Uri(dataFile));
        }
    }
}
