using Caliburn.Micro;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VU.ViewModels
{
    class LoginViewModel : Screen
    {
        public void Login(string username, string password)
        {
            //Egyszerű bejelenzkezés vizsgálat
            string dbpassword = "";
            MySqlCommand com = IoC.Get<ShellViewModel>().connection.CreateCommand();
            com.CommandText = "SELECT * FROM Users where username='" + username + "'";
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                username = reader.GetString(1);
                dbpassword = reader.GetString(2);
            }
            reader.Close();
            if(dbpassword == password)
            {
                IoC.Get<ShellViewModel>().ActivateItem(IoC.Get<CarListViewModel>());
            }
            else
            {
                MessageBox.Show("Wrong username or password!");
            }
            
        }
    }
}
