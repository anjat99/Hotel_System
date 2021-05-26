using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Hotel_System
{
    class Room
    {
        DBConnection conn = new DBConnection();
        //get all roomTypes
        public DataTable RoomTypeList()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM rooms_type", conn.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        //get all rooms based on type
        public DataTable RoomByType(int type)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM rooms WHERE type=@type and free = 'YES'", conn.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            command.Parameters.Add("@type", MySqlDbType.Int32).Value = type;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        //get room type id
        public int GetRoomType(int number)
        {
            MySqlCommand command = new MySqlCommand("SELECT type FROM rooms WHERE number=@number", conn.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            command.Parameters.Add("@number", MySqlDbType.Int32).Value = number;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return Convert.ToInt32(table.Rows[0][0].ToString());
        }

        //set free to NO/YES
        public bool SetRoomFree(int number, String isFree)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `rooms` SET `free`=@isFree' WHERE `number`=@number", conn.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            command.Parameters.Add("@number", MySqlDbType.Int32).Value = number;
            command.Parameters.Add("@isFree", MySqlDbType.VarChar).Value = isFree;

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

        //insert new room
        public bool InsertRoom(int number, int type, String phone, String free)
        {
            MySqlCommand command = new MySqlCommand();
            String queryInsert = "INSERT INTO `rooms`(`number`, `type`, `phone`, `free`) VALUES (@number, @type, @phone, @free)";
            command.CommandText = queryInsert;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@number", MySqlDbType.Int32).Value = number;
            command.Parameters.Add("@type", MySqlDbType.Int32).Value = type;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@free", MySqlDbType.VarChar).Value = free;

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

        //get all rooms
        public DataTable GetAllRooms()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM rooms", conn.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }


        //edit ROOM data
        public bool EditRoom(int number, int type, String phone, String free)
        {
            MySqlCommand command = new MySqlCommand();
            String queryUpdate = "UPDATE rooms SET type=@type, phone=@phone, free=@free WHERE number=@number";
            command.CommandText = queryUpdate;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@number", MySqlDbType.Int32).Value = number;
            command.Parameters.Add("@type", MySqlDbType.Int32).Value = type;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@free", MySqlDbType.VarChar).Value = free;

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

        //remove room
        public bool RemoveRoom(int number)
        {
            MySqlCommand command = new MySqlCommand();
            String queryDelete = "DELETE FROM rooms WHERE number=@number";
            command.CommandText = queryDelete;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@number", MySqlDbType.Int32).Value = number;

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
