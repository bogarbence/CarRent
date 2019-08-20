using Caliburn.Micro;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VU.Models;
using VU.Objects;

namespace VU.ViewModels
{
    
    class AddCarViewModel : Screen
    {
        #region Variables
        private Validators validate = new Validators();
        private bool _butEnable = false;
        public bool butEnable
        {
            get
            {
                return _butEnable;
            }
            set
            {
                _butEnable = value;
                NotifyOfPropertyChange(() => butEnable);
            }
        }
        private string _modell;
        public string modell
        {
            get
            {
                return _modell;
            }
            set
            {
                _modell = value;
                butValidator();
                NotifyOfPropertyChange(() => modell);
            }
        }

        private string _licensePlate;
        public string licensePlate
        {
            get
            {
                return _licensePlate;
            }
            set
            {
                _licensePlate = value;
                butValidator();
                NotifyOfPropertyChange(() => licensePlate);
            }
        }

        private string _price;
        public string price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                butValidator();
                NotifyOfPropertyChange(() => price);
            }
        }
        private string _status;
        public string status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                butValidator();
                NotifyOfPropertyChange(() => status);
            }
        }
        #endregion
        public void createCar()
        {
            //Autó class kreálása
            Car newCar = new Car();
            newCar.modell = modell;
            newCar.licenseplate = licensePlate;
            newCar.price = Convert.ToInt32(price);
            newCar.status = status;
            addCarToDB(newCar);
            MessageBox.Show("Car Added Succesfully!");
        }
        public void addCarToDB(Car newCar)
        {
            //Autó hozzáadása az adatbázishoz
            MySqlCommand com = IoC.Get<ShellViewModel>().connection.CreateCommand();
            com.CommandText = "INSERT INTO Cars(modell,licenseplate,price,status,available) VALUES(?modell,?licenseplate,?price,?status,?available)";
            com.Parameters.Add("?modell", MySqlDbType.VarChar).Value = newCar.modell;
            com.Parameters.Add("?licenseplate", MySqlDbType.VarChar).Value = newCar.licenseplate;
            com.Parameters.Add("?price", MySqlDbType.Int32).Value = newCar.price;
            com.Parameters.Add("?status", MySqlDbType.VarChar).Value = newCar.status;
            com.Parameters.Add("?available", MySqlDbType.VarChar).Value = "True";
            com.ExecuteNonQuery();
           
        }
        public void SelectedStatus(SelectionChangedEventArgs obj)
        {
            //Combobox bindingolásához szükséges segédmetódus
            status = (obj.AddedItems[0]).ToString();
            if (status.Contains("Intact"))
            {
                status = "Intact";
            }
            else
            {
                status = "Broken";
            }
        }
        public void butValidator()
        {
            //Validáció
            if (validate.IsNullOrEmpty(modell) || validate.IsNullOrEmpty(licensePlate) || validate.IsNullOrEmpty(price) || !validate.IsNumber(price) ||validate.IsNullOrEmpty(status))
            {
                butEnable = false;
            }
            else
            {
                butEnable = true;
            }
        }
    }
}
