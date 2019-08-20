﻿using Caliburn.Micro;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VU.Objects;

namespace VU.ViewModels
{
    class CarStatusViewModel: Screen
    {
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
        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }

        private ObservableCollection<Car> _CarList = IoC.Get<TestViewModel>().CarList;
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

        public void ChangeCarStatus()
        {
            MySqlCommand com = IoC.Get<ShellViewModel>().connection.CreateCommand();
            com.CommandText = "UPDATE Cars SET status=?status where id=" + SelectedCar.id;
            com.Parameters.Add("?status", MySqlDbType.VarChar).Value = Status;
            com.ExecuteNonQuery();   
        }

        public void Selected(SelectionChangedEventArgs obj)
        {
            try
            {
                SelectedCar = (Car)obj.AddedItems[0];
            } 
            catch
            {

            }
        }
        public void SelectedStatus(SelectionChangedEventArgs obj)
        {
            Status = (obj.AddedItems[0]).ToString();
            if(Status.Contains("Intact"))
            {
                Status = "Intact";
            }
            else
            {
                Status = "Broken";
            }
        }
    }
}
