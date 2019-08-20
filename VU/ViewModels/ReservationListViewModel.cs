using Caliburn.Micro;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VU.Objects;

namespace VU.ViewModels
{
    class ReservationListViewModel : Screen
    {
        private ObservableCollection<Reservation> _ResList = new ObservableCollection<Reservation>();
        public ObservableCollection<Reservation> ResList
        {
            get
            {
                return _ResList;
            }
            set
            {
                _ResList = value;
                NotifyOfPropertyChange(() => ResList);
            }
        }
        public ReservationListViewModel()
        {
            getReservationList();

        }
        public void getReservationList()
        {
            ResList.Clear();
            MySqlCommand com = IoC.Get<ShellViewModel>().connection.CreateCommand();
            com.CommandText = "SELECT * FROM Reservations";
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                Reservation newRes = new Reservation
                {
                    id = reader.GetInt32(0),
                    startDate = reader.GetDateTime(1),
                    endDate = reader.GetDateTime(2),
                    choosenCarID = reader.GetInt32(3),
                    comment = reader.GetString(4),

                };
                ResList.Add(newRes);
            }
            reader.Close();
            replaceCars();
        }
        public ObservableCollection<Reservation> refReservationList()
        {
            getReservationList();
            return ResList;
        }
        public void replaceCars()
        {
            for (int i = 0; i < ResList.Count; i++)
            {
                ResList[i].choosenCar = findCarByID(ResList[i].choosenCarID);
            }
        }
        public Car findCarByID(int id)
        {
            MySqlCommand com = IoC.Get<ShellViewModel>().connection.CreateCommand();
            com.CommandText = "SELECT * FROM Cars where id=" + id.ToString();
            MySqlDataReader reader = com.ExecuteReader();
            Car newCar = new Car();
            while (reader.Read())
            {
                newCar.id = reader.GetInt32(0);
                newCar.modell = reader.GetString(1);
                newCar.licenseplate = reader.GetString(2);
                newCar.price = reader.GetInt32(3);
                newCar.status = reader.GetString(4);
                newCar.available = reader.GetString(5);
            }
            reader.Close();
            return newCar;
        }
    }
}
