using System;
using MySql.Data.MySqlClient;

namespace BornToMove
{
    public interface ICrud
    {
        public abstract MySqlConnection Connect();

        public abstract bool IsConnected(MySqlConnection? conn);

        public abstract void CreateOneMove(string name, string description, int sweatRate);

        public abstract List<int>? ReadAllMoveIds();

        public abstract Move? ReadMoveById(int moveId);

        public abstract Dictionary<int, string>? ReadMoveNames();

        public abstract Dictionary<int, Move>? ReadAllMoves();

    }
}
