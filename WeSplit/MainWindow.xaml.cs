using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Trip.Children.Clear();
            Trip.Children.Add(new Home());
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            if (Menu.Width.ToString().Equals("60"))
            {
                Menu.Width = 240;
            }
            else
            {
                Menu.Width = 60;
            }
        }

        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (0 + (60 * index)), 0, 0);
        }

   

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            NewTrips t = new NewTrips();
            t.Show();
            this.Close();
        }

        private void ButtonTrip_Click ( object sender, RoutedEventArgs e )
        {
            Trip.Children.Clear();
            Trip.Children.Add(new Home());
        }

        private void ButtonFinish_Click ( object sender, RoutedEventArgs e )
        {
            Trip.Children.Clear();
            Trip.Children.Add(new Finish());
        }

        private void _trip_Navigated ( object sender, NavigationEventArgs e )
        {

        }
    }
}