using System;
using MySql.Data.MySqlClient;

namespace BornToMove
{
    public interface ICrud
    {
        public abstract MySqlConnection Connect();

        public abstract bool IsConnected(MySqlConnection? conn);

        public abstract void CreateMove(string name, string description, int sweatRate);

        public abstract List<int>? ReadMoveIds();

        public abstract Move? ReadMoveById(int moveId);

        public abstract Dictionary<int, string>? ReadAllMoveNames();

        public abstract Dictionary<int, Move>? ReadAllMoves();

    }
}
