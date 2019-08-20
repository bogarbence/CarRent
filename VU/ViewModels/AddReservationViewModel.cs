using Caliburn.Micro;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VU.Models;
using VU.Objects;
using Xceed.Wpf.Toolkit;

namespace VU.ViewModels
{
    class AddReservationViewModel : Screen
    {
        #region Variables
        private Validators validate = new Validators();
        private bool _butEnable = false;
        private DateTime _StartDate = DateTime.Now;
        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
                NotifyOfPropertyChange(() => StartDate);
            }
        }
        private Car _selectedCar;
        public Car SelectedCar
        {
            get
            {
                return _selectedCar;
            }
            set
            {
                _selectedCar = value;
                NotifyOfPropertyChange(() => SelectedCar);
            }
        }
        private DateTime _EndDate = DateTime.Now;
        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value;
                NotifyOfPropertyChange(() => EndDate);
            }
        }
        private string _Price;
        public string Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
                NotifyOfPropertyChange(() => Price);
            }
        }
        private string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                NotifyOfPropertyChange(() => Comment);
            }
        }

        private ObservableCollection<Car> _CarList = IoC.Get<CarListViewModel>().CarList;
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
        #endregion

        public void CreateReservation()
        {
            //Foglalás osztály kreálása
            CarList = IoC.Get<CarListViewModel>().CarList;
            Reservation newRes = new Reservation();
            newRes.choosenCar = SelectedCar;
            newRes.startDate = StartDate;
            newRes.endDate = EndDate;
            newRes.comment = Comment;
            if (Validate(newRes))
            {
                addReservationToDB(newRes);
                MessageBox.Show("Succesful Reservation!");
            }

        }
        public void addReservationToDB(Reservation newRes)
        {
            //Foglalás hozzáadása az adatbázishoz
            MySqlCommand com = IoC.Get<ShellViewModel>().connection.CreateCommand();
            com.CommandText = "INSERT INTO Reservations(startDate,endDate,choosenCarID,comment) VALUES(?startDate,?endDate,?choosenCarID,?comment)";
            com.Parameters.Add("?startDate", MySqlDbType.DateTime).Value = newRes.startDate;
            com.Parameters.Add("?endDate", MySqlDbType.DateTime).Value = newRes.endDate;
            com.Parameters.Add("?choosenCarID", MySqlDbType.Int32).Value = newRes.choosenCar.id;
            com.Parameters.Add("?comment", MySqlDbType.VarChar).Value = newRes.comment;
            com.ExecuteNonQuery();

        }
        public void calculatePrice()
        {
            //Ár kiszámítása
            Double diff = (EndDate - StartDate).TotalHours;
            diff = (Math.Ceiling(diff)) * SelectedCar.price;
            if (diff <= 0)
            {
                Price = "End date can't be earlier than Start Date!";
            }
            else
            {
                Price = diff.ToString();
            }
        }
        public void Selected(SelectionChangedEventArgs obj)
        {
            //Combobox bindingolásához szükséges segédmetódus
            try
            {
                SelectedCar = (Car)obj.AddedItems[0];
            }
            catch
            {

            }
        }
        public bool Validate(Reservation newRes)
        {
            //Validáció
            if (((ObservableCollection<Reservation>)(IoC.Get<ReservationListViewModel>().ResList)).Count == 0)
            {
                IoC.Get<ReservationListViewModel>().getReservationList();
            }
            if (newRes.choosenCar.status == "Broken")
            {
                MessageBox.Show("This car is broken");
                return false;
            }
            for (int i = 0; i < ((ObservableCollection<Reservation>)(IoC.Get<ReservationListViewModel>().ResList)).Count; i++)
            {
                Reservation Res = ((ObservableCollection<Reservation>)(IoC.Get<ReservationListViewModel>().ResList))[i];
                if ((Res.choosenCar.licenseplate == newRes.choosenCar.licenseplate))
                {
                    if ((newRes.startDate.Ticks > Res.startDate.Ticks && newRes.startDate.Ticks < Res.endDate.Ticks) || (newRes.endDate.Ticks > Res.startDate.Ticks && newRes.startDate.Ticks < Res.endDate.Ticks))
                    {
                        MessageBox.Show("This car is taken for that time!");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
