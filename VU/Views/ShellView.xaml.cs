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
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : UserControl
    {
        public ShellView()
        {
            InitializeComponent();
        }
        private void CarList(object sender, RoutedEventArgs e)
        {
            IoC.Get<ShellViewModel>().ActivateItem(IoC.Get<TestViewModel>());
            IoC.Get<TestViewModel>().getCarList();
        }

        private void AddCar(object sender, RoutedEventArgs e)
        {
            IoC.Get<ShellViewModel>().ActivateItem(IoC.Get<AddCarViewModel>());
        }

        private void AddReservation(object sender, RoutedEventArgs e)
        {
            IoC.Get<AddReservationViewModel>().SelectedCar = null;
            IoC.Get<AddReservationViewModel>().CarList = IoC.Get<TestViewModel>().refCarList();
            IoC.Get<ShellViewModel>().ActivateItem(IoC.Get<AddReservationViewModel>());
        }

        private void ChangeCarStatus(object sender, RoutedEventArgs e)
        {
            IoC.Get<ShellViewModel>().ActivateItem(IoC.Get<CarStatusViewModel>());
        }

        private void ReservationList(object sender, RoutedEventArgs e)
        {
            IoC.Get<ShellViewModel>().ActivateItem(IoC.Get<ReservationListViewModel>());
        }

        private void Riports(object sender, RoutedEventArgs e)
        {
            IoC.Get<RiportsViewModel>().ResList = IoC.Get<ReservationListViewModel>().refReservationList();
            IoC.Get<RiportsViewModel>().CarList = IoC.Get<TestViewModel>().refCarList();
            IoC.Get<ShellViewModel>().ActivateItem(IoC.Get<RiportsViewModel>());
        }
    }
}
