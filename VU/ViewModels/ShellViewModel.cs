using Caliburn.Micro;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VU.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public MySqlConnection connection = null;
        public ShellViewModel()
        {
           SqlConnect();
           ActivateItem(new LoginViewModel());
        }
        public void SqlConnect()
        {
            string connstring = string.Format("Server=sql7.freemysqlhosting.net; database=sql7301930; UID=sql7301930; password=m8NqUDKKNQ");
            connection = new MySqlConnection(connstring);
            connection.Open();
        }
    }
}
