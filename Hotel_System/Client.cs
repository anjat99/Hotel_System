using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Hotel_System
{

    /*
     * class for insert/update/delete/get all client
     * 
     * */
    class Client
    {
        DBConnection conn = new DBConnection();

        //insert new client
        public bool InsertClient(String fname, String lname, String phone, String country)
        {
            MySqlCommand command = new MySqlCommand();
            String queryInsert = "INSERT INTO clients(first_name,last_name,phone,country) VALUES (@fname, @lname, @phone, @country)";
            command.CommandText = queryInsert;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@country", MySqlDbType.VarChar).Value = country;

            conn.OpenConnection();
            if(command.ExecuteNonQuery() == 1)
            {
                conn.CloseConnection();
                return true;
            }
            else
            {
                conn.CloseConnection();
                return false;
            }
        }

        //get all clients
        public DataTable GetAllClients()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM clients", conn.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        //edit client data
        public bool EditClient(int id, String fname, String lname, String phone, String country)
        {
            MySqlCommand command = new MySqlCommand();
            String queryUpdate = "UPDATE clients SET first_name=@fname, last_name=@lname, phone=@phone, country=@country WHERE id=@cid";
            command.CommandText = queryUpdate;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = id; 
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@country", MySqlDbType.VarChar).Value = country;

            conn.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                conn.CloseConnection();
                return true;
            }
            else
            {
                conn.CloseConnection();
                return false;
            }
        }

        //remove client
        public bool RemoveClient(int id)
        {
            MySqlCommand command = new MySqlCommand();
            String queryDelete = "DELETE FROM clients WHERE id=@cid";
            command.CommandText = queryDelete;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = id; 

            conn.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                conn.CloseConnection();
                return true;
            }
            else
            {
                conn.CloseConnection();
                return false;
            }
        }
    }
}
