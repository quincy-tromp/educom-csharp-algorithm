using System;
using BornToMove.DAL;

namespace BornToMove.Business
{
	public class BuMove
	{
        // Fields
        private MoveCrud crud;
        // Properties
        public IPresenter presenter;
        public Move? selectedMove;
        public Dictionary<int, string> moveNames = new Dictionary<int, string>();
        public int initialChoice = -1;
        public string nameOfMoveChosenFromList = "";
        public int choiceFromList = -1;
        public int userReview = -1;
        public int userIntensity = -1;

        // Constructor
        public BuMove(MoveCrud crud)
		{
            this.crud = crud;
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
            return (moveNames.ContainsKey(choiceFromList) || choiceFromList == 0);
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
                selectedMove = crud.ReadMoveById(random.Next(1, moveIds.Count));
            }
        }

        /// <summary>
		/// Sets moveNames
		/// </summary>
        public void GetMoveNameList()
        {
            var names = crud.ReadMoveNames();
            if (names != null)
            {
                for (int i = 1; i < names.Count + 1; i++)
                {
                    moveNames.Add(i, names[i - 1]);
                }
            }
        }

        /// <summary>
        /// Validates the new move name
        /// </summary>
        /// <param name="newMoveName">The name of the new move</param>
        /// <returns>A boolean with True if move already exists, or False if move doesn't exist</returns>
        public bool ValidateNewMoveName(string newMoveName)
        {
            return (moveNames.ContainsValue(newMoveName));
        }

        public bool ValidateNewMoveSweatRate(int newMoveSweatRate)
        {
            return (newMoveSweatRate >= 1 && newMoveSweatRate <= 5);
        }

        /// <summary>
		/// Creates new move
		/// </summary>
        /// <param name="newMove">A Move object with new move values</param>
        public void CreateNewMove(Move newMove)
        {
            crud.CreateMove(newMove);
        }

        /// <summary>
		/// Sets SelectedMove based on chosenMoveId
		/// </summary>
        ///<param name="moveName">The name of the move</param>
        public void SetSelectedMove(string moveName)
        {
            try
            {
                selectedMove = crud.ReadMoveByName(moveName)[0];
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
        public bool ValidateUserReview(int userReview)
        {
            return (userReview >= 1 && userReview <= 5);
        }

        /// <summary>
        /// Validates the insentity given by user
        /// </summary>
        /// <param name="userIntensity">The intensity given by user</param>
        /// <returns>A boolean with True if intensity is valid, or False if not valid</returns>
        public bool ValidateUserIntensity(int userIntensity)
        {
            return (userIntensity >= 1 && userIntensity <= 5);
        }

    }
}

