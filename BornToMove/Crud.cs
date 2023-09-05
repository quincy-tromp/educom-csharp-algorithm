using System;
using MySql.Data.MySqlClient;

namespace BornToMove
{
	public class Crud
	{
		static MySqlConnection conn;

		public MySqlConnection Connect()
		{
			try
			{
				var connString = "Server=localhost;Database=born2move;Uid=born2move_user;Pwd=IRo4HiMfdl!x2VWN;";
				MySqlConnection conn = new MySqlConnection(connString);
                Console.WriteLine("Connected successfully.");
                return conn;
            }
			catch (Exception e)
			{
				Console.WriteLine("Not successful due to " + e.ToString());
				return null;
			}
        }

		public void createMovement(string name, string description, int sweatRate)
		{
			MySqlConnection conn = Connect();
			var sql = "INSERT INTO move (name, description, sweatRate)" +
				"VALUES (@name, @description, @sweatRate);";
			MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", name);
			cmd.Parameters.AddWithValue("@description", description);
			cmd.Parameters.AddWithValue("@sweatRate", sweatRate);
           
            try
			{
				conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
				Console.WriteLine("Movement created successfully.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to insert data due to " + e.Message);
			}
		}

		public Dictionary<int, Move> getMovements()
		{
			Dictionary<int, Move> moves = new Dictionary<int, Move>();
			MySqlConnection conn = Connect();
			var sql = "SELECT * FROM move;";
			MySqlCommand cmd = new MySqlCommand(sql, conn);

			try
			{
				conn.Open();
				MySqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					int id = rdr.GetInt32(0);
					string name = rdr.GetString(1);
					string description = rdr.GetString(2);
					int sweatRate = rdr.GetInt32(3);
					moves.Add(id, new Move(id, name, description, sweatRate));
					Console.WriteLine("Movement read successfully.");
				}
                return moves;
            }
			catch (Exception e)
			{
				Console.WriteLine("Failed to read data due to " + e.Message);
				return null;
			}
			finally
			{
				conn.Close();
			}
		}
	}
}

