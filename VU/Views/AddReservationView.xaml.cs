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
    /// Interaction logic for AddReservationView.xaml
    /// </summary>
    public partial class AddReservationView : UserControl
    {
        public AddReservationView()
        {
            InitializeComponent();
        }

        private void AddRevBtn_Click(object sender, RoutedEventArgs e)
        {
            IoC.Get<AddReservationViewModel>().CreateReservation();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                IoC.Get<AddReservationViewModel>().calculatePrice();
            }
            catch
            {

            }
        }

        private void DateTimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                IoC.Get<AddReservationViewModel>().calculatePrice();
            }
            catch
            {

            }
        }

        private void DateTimePicker_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                IoC.Get<AddReservationViewModel>().calculatePrice();
            }
            catch
            {

            }
        }
    }
}
