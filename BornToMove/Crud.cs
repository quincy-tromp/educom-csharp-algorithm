using System;
using MySql.Data.MySqlClient;

namespace BornToMove
{
    public class Crud : ICrud
    {
        /// <summary>
		/// Connects to MySQL database
		/// </summary>
        ///
        /// <returns>MySqlConnection object</returns>
        public MySqlConnection Connect()
        {
            var connString = "Server=localhost;Database=born2move;Uid=born2move_user;Pwd=IRo4HiMfdl!x2VWN;";
            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }

        /// <summary>
		/// Checks MySqlConnection for connection
		/// </summary>
        ///
        /// <params name="conn">MySqlConnection object to check</params>
        ///
        /// <returns>TRUE if MySqlConnection is NOT Null / FALSE if MySqlConnection is Null</returns>
        public bool IsConnected(MySqlConnection? conn)
        {
            if (conn == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
		/// Creates a new move in DB
		/// </summary>
        /// 
        /// <params name="name">Name of the move</params>
        /// <params name="description">Description of the move</params>
        /// <param name="sweatRate">The sweatRate of the move</param>

        public void CreateOneMove(string name, string description, int sweatRate)
        {
            var conn = Connect();
            if (IsConnected(conn))
            {
                var sql = "INSERT INTO move (name, description, sweatRate)" +
                "VALUES (@name, @description, @sweatRate);";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@sweatRate", sweatRate);

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                Console.WriteLine("Move created successfully.");
            }
        }

        /// <summary>
		/// Reads all moves IDs
		/// </summary>
        /// 
        ///<returns>List of move IDs as integers / Null if no IDs found in DB</returns>
        public List<int>? ReadAllMoveIds()
        {
            List<int> moveIds = new List<int>();
            var conn = Connect();
            if (IsConnected(conn))
            {
                var sql = "SELECT id FROM move;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32(0);
                        moveIds.Add(id);
                    }
                    return moveIds;
                }
                finally
                {
                    conn.Close();
                }
            }
            return null;
        }

        /// <summary>
		/// Read one move by ID
		/// </summary>
        ///
        /// <param name="moveId">The ID of the move to search for</param>
        /// 
        ///<returns>Move object / Null if no move found in DB</returns>
        public Move? ReadMoveById(int moveId)
        {
            var conn = Connect();
            if (IsConnected(conn))
            {
                var sql = "SELECT * FROM move WHERE id = @id;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", moveId);

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
                        return new Move(id, name, description, sweatRate);
                    }
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
            return null;
        }

        /// <summary>
		/// Read all move names
		/// </summary>
        /// 
        ///<returns>Dictionary with move ID as key and move name as value / Null if no move found in DB</returns>
        public Dictionary<int, string>? ReadMoveNames()
        {
            Dictionary<int, string> moveNames = new Dictionary<int, string>();
            var conn = Connect();
            if (IsConnected(conn))
            {
                var sql = "SELECT id, name FROM move;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32(0);
                        string name = rdr.GetString(1);
                        moveNames.Add(id, name);
                    }
                    return moveNames;
                }
                finally
                {
                    conn.Close();
                }
            }
            return null;
        }

        /// <summary>
		/// Read one move by ID
		/// </summary>
        /// 
        ///<returns>Dictionary with move ID as key and Move object as value / Null if no move found in DB</returns>
        public Dictionary<int, Move>? ReadAllMoves()
        {
            Dictionary<int, Move> moves = new Dictionary<int, Move>();
            var conn = Connect();
            if (IsConnected(conn))
            {
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
                    }
                    return moves;
                }
                finally
                {
                    conn.Close();
                }
            }
            return null;
        }
    }
}

