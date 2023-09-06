using System;

namespace BornToMove
{
	public class MoveCrud
	{
        // Properties
		public Crud crud;

        // Constructor
		public MoveCrud(Crud crud)
		{
			this.crud = crud;
		}

        /// <summary>
        /// Gets all move names
        /// </summary>
        /// <returns>Dictionary with move ID as key and move name as value / Null if no move found in DB</returns>
		public Dictionary<int, string>? GetMoveNames()
		{
			try
			{
                return crud.ReadMoveNames();
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
        /// <returns>List with move IDs / Null if no move found in DB</returns>
		public List<int>? GetAllMoveIds()
		{
            try
            {
                return crud.ReadAllMoveIds();
            }
            catch (Exception e)
            {
                Console.WriteLine("Process failed due to technical error: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a move by the move ID
        /// </summary>
        /// <param name="id">The ID of the move</param>
        /// <returns>Move object / Null if no move found in DB</returns>
        public Move? GetMoveById(int id)
		{
			return crud.ReadMoveById(id);
		}

        /// <summary>
        /// Creates a new move in DB
        /// </summary>
        /// <param name="name">The name of the new move</param>
        /// <param name="sweatRate">The sweatRate of the new move</param>
        /// <param name="description">The description of the new move</param>
		public void CreateOneMove(string name, int sweatRate, string description)
		{
			try
			{
                crud.CreateOneMove(name, description, sweatRate);
            }
            catch (Exception e)
            {
                Console.WriteLine("Process failed due to technical error: " + e.Message);
            }
        }
	}
}

