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
    /// Interaction logic for CarStatusView.xaml
    /// </summary>
    public partial class CarStatusView : UserControl
    {
        public CarStatusView()
        {
            InitializeComponent();
        }

        private void ChangeStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            IoC.Get<CarStatusViewModel>().ChangeCarStatus();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
