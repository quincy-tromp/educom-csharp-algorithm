using System;
using System.Linq;

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
        /// Reads all moves in DB
        /// </summary>
        /// <returns>A List<Move> with move objects, or Null if no moves found in DB</Move></returns>
        public List<Move>? ReadAllMoves()
        {
            try
            {
                return context.Move.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Process failed due to technical error: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets all move names
        /// </summary>
        /// <returns>A List<string> with move names</string>, or Null if no move found in DB</returns>
		public List<string?> ReadMoveNames()
        {
            List<string?> names = new List<string?>();
            try
            {
                names = context.Move.Select(move => move.Name).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Process failed due to technical error: " + e.Message);
            }
            return names;
        }

        /// <summary>
        /// Gets all move IDs
        /// </summary>
        /// <returns>A List<int> with move IDs, or Null if no move found in DB</returns>
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
        /// <param name="id">The ID of the move</param>
        /// <returns>Move object, or Null if no move found in DB</returns>
        public Move? ReadMoveById(int id)
        {
            return context.Move.Find(id);
        }

        /// <summary>
        /// Gets a move by name of the move
        /// </summary>
        /// <param name="name">The name of the move</param>
        /// <returns>Move object, or Null if no move found in DB</returns>
        public List<Move>? ReadMoveByName(string name)
        {
            return context.Move.Where(move => move.Name == name).ToList();
        }

        /// <summary>
        /// Creates a new move in DB
        /// </summary>
        /// <param name="newMove">The new move to be created</param>
		public void CreateMove(Move newMove)
        {
            try
            {
                context.Move.Add(newMove);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Process failed due to technical error: " + e.Message);
            }
        }

        /// <summary>
        /// Deletes a move in DB
        /// </summary>
        /// <param name="id">The Id of the move to delete</param>
        public void DeleteMove(int id)
        {
            context.Move.Remove(new Move { Id = id });
            context.SaveChanges();
        }

        /// <summary>
        /// Updates a move in DB
        /// </summary>
        /// <param name="updatedMove">The move with updated values. Note: ID is required in updatedMmove</param>
        public void UpdateMove(Move updatedMove)
        {
            context.Move.Update(updatedMove);
            context.SaveChanges();
        }

        /// <summary>
        /// Checks if move table in DB is empty
        /// </summary>
        /// <returns>A boolean True if move table is empty, or False if not empty</returns>
        public bool IsMoveEmpty()
        {
            return (context.Move.Count() == 0);
        }

        /// <summary>
        /// Creates a new move rating in DB
        /// </summary>
        /// <param name="newMoveRating">The new move rating to be created</param>
        public void CreateMoveRating(MoveRating newMoveRating)
        {
            context.MoveRating.Add(newMoveRating);
            context.SaveChanges();
        }

        /// <summary>
        /// Reads the average rating by move Id
        /// </summary>
        /// <param name="moveId">The Id of the move to read average rating for</param>
        /// <returns>A double for move average rating</returns>
        public double ReadAverageRating(int moveId)
        {
            return (from rating in context.MoveRating
                    where rating.Move.Id == moveId
                    select rating).Average(r => r.Rating);
        }
	}
}

