using System;

namespace BornToMove
{
	public class MoveCrud
	{
		public Crud crud;

		public MoveCrud(Crud crud)
		{
			this.crud = crud;
		}

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

        public Move? GetMoveById(int id)
		{
			return crud.ReadMoveById(id);
		}

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

