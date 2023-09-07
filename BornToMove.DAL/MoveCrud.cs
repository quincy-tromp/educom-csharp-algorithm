using System;

namespace BornToMove.DAL
{
	public class MoveCrud
	{
        // Properties
        private MoveContext context;

        // Constructor
		public MoveCrud(MoveContext context)
		{
			this.context = context;
		}

        /// <summary>
        /// Gets all move names
        /// </summary>
        /// 
        /// <returns>A List<string> with move names</string> / Null if no move found in DB</returns>
		public List<string>? ReadMoveNames()
		{
			try
			{
                return context.Move.Select(move => move.Name).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Process failed due to technical error: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets all move IDs
        /// </summary>
        /// 
        /// <returns>A List<int> with move IDs / Null if no move found in DB</returns>
		public List<int>? ReadMoveIds()
		{
            try
            {
                return context.Move.Select(move => move.Id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Process failed due to technical error: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a move by ID of the move
        /// </summary>
        /// 
        /// <param name="id">The ID of the move</param>
        /// <returns>Move object / Null if no move found in DB</returns>
        public Move? ReadMoveById(int id)
		{
            return context.Move.Find(id);
		}

        /// <summary>
        /// Gets a move by name of the move
        /// </summary>
        /// 
        /// <param name="name">The name of the move</param>
        /// <returns>Move object / Null if no move found in DB</returns>
        public List<Move>? ReadMoveByName(string name)
        {
            return context.Move.Where(move => move.Name == name).ToList();
        }

        /// <summary>
        /// Creates a new move in DB
        /// </summary>
        /// 
        /// <param name="move">The new move to be created</param>
		public void CreateMove(Move move)
		{
			try
			{
                context.Move.Add(move);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Process failed due to technical error: " + e.Message);
            }
        }
	}
}

