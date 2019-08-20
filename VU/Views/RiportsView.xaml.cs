using Caliburn.Micro;
using System;
using System.Collections.Generic;
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
using VU.ViewModels;

namespace VU.Views
{
    /// <summary>
    /// Interaction logic for RiportsView.xaml
    /// </summary>
    public partial class RiportsView : UserControl
    {
        public RiportsView()
        {
            InitializeComponent();
        }

        private void BrokenCars(object sender, RoutedEventArgs e)
        {
            IoC.Get<RiportsViewModel>().GenerateBrokenCarList();
        }

        private void WeeklyRes(object sender, RoutedEventArgs e)
        {
            IoC.Get<RiportsViewModel>().GenerateWeeklyReservations();
        }

        private void MProfitCar(object sender, RoutedEventArgs e)
        {
            IoC.Get<RiportsViewModel>().GenerateMostProfitableCar();
        }

        private void DateTimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
