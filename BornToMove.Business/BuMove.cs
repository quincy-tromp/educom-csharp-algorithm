using System;
using BornToMove.DAL;
using Organizer;

namespace BornToMove.Business
{
	public class BuMove
	{
        // Fields
        private MoveCrud crud;
        private RotateSort<MoveAverageRating> sorter;
        // Properties
        public MoveAverageRating selectedMove = new MoveAverageRating();
        public List<MoveAverageRating> moveChoiceList= new List<MoveAverageRating>();
        public int initialChoice = -1;
        public Move moveChosenFromList;
        public int choiceFromList = -1;
        public double userRating = -1;
        public double userIntensity = -1;
        public double averageRating = 0;

        // Constructor
        public BuMove(MoveCrud crud)
		{
            this.crud = crud;
            this.sorter = new RotateSort<MoveAverageRating>();
		}

        /// <summary>
        /// Validates the initial choice
        /// </summary>
        /// <param name="initialChoice">The inital choice made by user</param>
        /// <returns>A boolean with True if initial choice is valid, or False if not valid</returns>
        public bool ValidateInitialChoice(int initialChoice)
        {
            return (initialChoice == 1 || initialChoice == 2);
        }

        /// <summary>
        /// Validates the choice from list
        /// </summary>
        /// <param name="choiceFromList">The choice from list made by user</param>
        /// <returns>A boolean with True if chioice is valid, or False if not valid</returns>
        public bool ValidateChoiceFromList(int choiceFromList)
        {
            if (choiceFromList == 0) { return true; };

            foreach (MoveAverageRating move in moveChoiceList)
            {
                if (move.Move.Id == choiceFromList)
                {
                    moveChosenFromList = move.Move;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
		/// Generates a move suggestion for user/Sets chosenMove
		/// </summary>
        public void GenerateMoveSuggestion()
        {
            var moveIds = crud.ReadMoveIds();
            if (moveIds != null)
            {
                Random random = new Random();
                var id = random.Next(1, moveIds.Count);
                selectedMove.Move = crud.ReadMoveById(id);
                selectedMove.AverageRating = crud.ReadAverageRatingById(id);
            }
        }

        /// <summary>
		/// Gets the move choice list
		/// </summary>
        public void GetMoveChoiceList()
        {
            List<Move>? moves = crud.ReadAllMoves();

            if (moves != null)
            {
                foreach (Move move in moves)
                {
                    moveChoiceList.Add(new MoveAverageRating()
                    {
                        Move = move,
                        AverageRating = crud.ReadAverageRatingById(move.Id)
                    });
                }
                moveChoiceList = sorter.Sort(moveChoiceList, new RatingsConverter());
            }
        }

        /// <summary>
        /// Validates the new move name
        /// </summary>
        /// <param name="newMoveName">The name of the new move</param>
        /// <returns>A boolean with True if move already exists, or False if move doesn't exist</returns>
        public bool ValidateNewMoveName(string newMoveName)
        {
            foreach (MoveAverageRating move in moveChoiceList)
            {
                if (move.Move.Name == newMoveName) { return true; }
            }
            return false;
        }

        public bool ValidateNewMoveSweatRate(int newMoveSweatRate)
        {
            return (newMoveSweatRate >= 1 && newMoveSweatRate <= 5);
        }

        /// <summary>
		/// Creates new move
		/// </summary>
        /// <param name="newMove">A Move object with new move values</param>
        public void SaveMove(Move newMove)
        {
            crud.CreateMove(newMove);
        }

        /// <summary>
		/// Gets Selected move
		/// </summary>
        public void GetSelectedMove()
        {
            try
            {
                selectedMove.Move = moveChosenFromList;
                selectedMove.AverageRating = crud.ReadAverageRatingById(moveChosenFromList.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine("Process failed due to technical error: " + e.Message);
            }
        }

        /// <summary>
        /// Validates the review made by user
        /// </summary>
        /// <param name="userReview">The user review</param>
        /// <returns>A boolean with True if review is valid, or False if not valid</returns>
        public bool ValidateUserRating(double userReview)
        {
            return (userReview >= 1.0 && userReview <= 5.0);
        }

        /// <summary>
        /// Validates the insentity given by user
        /// </summary>
        /// <param name="userIntensity">The intensity given by user</param>
        /// <returns>A boolean with True if intensity is valid, or False if not valid</returns>
        public bool ValidateUserIntensity(double userIntensity)
        {
            return (userIntensity >= 1.0 && userIntensity <= 5.0);
        }

        /// <summary>
        /// Creates initial moves 
        /// </summary>
        public void CreateInitialMoves()
        {
            List<Move> moves = new List<Move>();
            moves.Add(new Move
            {
                Name = "Push Up",
                SweatRate = 3,
                Description = "Ga horizontaal liggen op teentoppen en handen. Laat het lijf langzaam zakken tot de neus de grond bijna raakt. " +
                "Duw het lijf terug nu omhoog tot de ellebogen bijna gestrekt zijn. Vervolgens weer laten zakken. Doe dit 20 keer zonder tussenpauzes"
            });
            moves.Add(new Move
            {
                Name = "Planking",
                SweatRate = 3,
                Description = "Ga horizontaal liggen op teentoppen en onderarmen. Houdt deze positie 1 minuut vast"
            });
            moves.Add(new Move
            {
                Name = "Squat",
                SweatRate = 5,
                Description = "Ga staan met gestrekte armen. Zak door de knieën tot de billen de grond bijna raken. " +
                "Ga weer volledig gestrekt staan. Herhaal dit 20 keer zonder tussenpauzes"
            });

            try
            {
                foreach (Move move in moves)
                {
                    crud.CreateMove(move);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Process failed due to technical error: " + ex.Message);
            }
        }

        /// <summary>
        /// Lets Crud create initial moves if move table is empty
        /// </summary>
        public void StartupMoves()
        {
            if (crud.IsMoveEmpty())
            {
                CreateInitialMoves();
            }
        }

        /// <summary>
        /// Lets Crud add new move rating to DB
        /// </summary>
        public void AddMoveRating()
        {
            try
            {
                crud.CreateMoveRating(new MoveRating
                {
                    Move = selectedMove.Move,
                    Rating = userRating,
                    Vote = userIntensity
                }) ;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Process failed due to technical error: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the average rating for selected move
        /// </summary>
        public void GetAverageRating()
        {
            try
            {
                averageRating = crud.ReadAverageRatingById(selectedMove.Move.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Process failed due to technical error: " + ex.Message);
            }
        }
    }
}

