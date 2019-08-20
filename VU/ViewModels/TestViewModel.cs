using Caliburn.Micro;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VU.Objects;

namespace VU.ViewModels
{
    public class TestViewModel : Screen
    {
        private ObservableCollection<Car> _CarList = new ObservableCollection<Car>();
        public ObservableCollection<Car> CarList
        {
            get
            {
                return _CarList;
            }
            set
            {
                _CarList = value;
                NotifyOfPropertyChange(() => CarList);
            }
        }


        public TestViewModel()
        {
            getCarList();

        }
        public void getCarList()
        {
            CarList.Clear();
            MySqlCommand com = IoC.Get<ShellViewModel>().connection.CreateCommand();
            com.CommandText = "SELECT * FROM Cars";
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                Car newCar = new Car
                {
                    id = reader.GetInt32(0),
                    modell = reader.GetString(1),
                    licenseplate = reader.GetString(2),
                    price = reader.GetInt32(3),
                    status = reader.GetString(4),
                    available = reader.GetString(5)
                };
                CarList.Add(newCar);
            }
            reader.Close();
        }
        public ObservableCollection<Car> refCarList()
        {
            getCarList();
            return CarList;
        }
    }
}
