using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using LiveCharts;
using LiveCharts.Wpf;


namespace WeSplit
{
    /// <summary>
    /// Interaction logic for USChart.xaml
    /// </summary>
    public partial class USChart : UserControl
    {
        public Trips _data;
        string nameTrip;
        public USChart(Trips t)
        {
            InitializeComponent();
            _data = t;
            nameTrip = _data.Name;
        }
        public Func<ChartPoint, string> PointLabel => point => $"{point.Y} ({point.Participation:P1})";
        public SeriesCollection Data1 { get; set; }
        public SeriesCollection Data2 { get; set; }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            Title.Text = _data.Name;
            Data1 = new SeriesCollection() { };
            Data2 = new SeriesCollection() { };
            String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            appStartPath = appStartPath + $"\\ListTrips\\{nameTrip}\\";
            string fileExpenses = appStartPath + $"Expenses.txt";
            var readExpenses = File.ReadAllLines(fileExpenses);
            for (int i = 0; i < readExpenses.Length; i = i + 2)
            {
                string tenkhoanchi = readExpenses[i];
                float tienchi = float.Parse(readExpenses[i + 1]);
                var t = new PieSeries()
                {
                    Values = new ChartValues<float> { tienchi },
                    Title = tenkhoanchi,
                    //DataLabels = true,
                    //LabelPoint = PointLabel
                };
                Data1.Add(t);
            }

            string fileMembers = appStartPath + $"Members.txt";
            var readMembers = File.ReadAllLines(fileMembers);
            for (int i = 0; i < readMembers.Length; i = i + 2)
            {
                string tenthanhvien = readMembers[i];
                float tienthu = float.Parse(readMembers[i + 1]);
                var t = new PieSeries()
                {
                    Values = new ChartValues<float> { tienthu },
                    Title = tenthanhvien,
                    //DataLabels = true,
                    //LabelPoint = PointLabel
                };
                Data2.Add(t);
            }
            this.DataContext = this;
        }

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            var chart = chartPoint.ChartView as PieChart;
            foreach (PieSeries pie in chart.Series)
            {
                pie.PushOut = 0;
            }

            var neo = chartPoint.SeriesView as PieSeries;
            neo.PushOut = 10;
        }

        private void imgBack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            USPieChart.Children.Clear();
            USPieChart.Children.Add(new TripsDetail(_data));
        }
    }
}
