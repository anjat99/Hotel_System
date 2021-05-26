using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_System
{
    /*
     * connection between app and MySQL database
     */
    class DBConnection
    {
        private MySqlConnection _connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=hotel_system");

        //return connection
        public MySqlConnection GetConnection()
        {
            return _connection;
        }

        //open connection
        public void OpenConnection()
        {
            if(_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        } 
        
        //close connection
        public void CloseConnection()
        {
            if(_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
