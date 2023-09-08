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
		/// Sets userChoice
		/// </summary>
        public void SetInitialChoice()
        {
            initialChoice = presenter.view.AskForNumber();
            while (!(initialChoice == 1 || initialChoice == 2))
            {
                presenter.view.DisplayTryAgain("");
                initialChoice = presenter.view.AskForNumber();
            }
        }

        /// <summary>
		/// Sets moveChoice
		/// </summary>
        public void ChooseMoveFromList(Dictionary<int, string> moveNames)
        {
            choiceFromList = presenter.view.AskForNumber();
            while (!(moveNames.ContainsKey(choiceFromList) || choiceFromList == 0))
            {
                presenter.view.DisplayTryAgain("");
                choiceFromList = presenter.view.AskForNumber();
            }
            nameOfMoveChosenFromList = moveNames[choiceFromList];
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
		/// Sets newMoveName
		/// </summary>
        private string GetNewMoveName()
        {
            presenter.view.AskForThis("move name");
            string newName = presenter.view.AskForString();

            while (moveNames.ContainsValue(newName))
            {
                presenter.view.DisplayTryAgain("Move already exists. ");
                newName = presenter.view.AskForString();
            }
            return newName;
        }

        /// <summary>
		/// Sets newMoveSweatRate
		/// </summary>
        private int GetNewMoveSweatRate()
        {
            presenter.view.AskForThis("sweatRate");
            int newSweatRate = presenter.view.AskForNumber();
            while (!(newSweatRate >= 1 && newSweatRate <= 5))
            {
                presenter.view.DisplayTryAgain("SweatRate should be between 1 and 5. ");
                newSweatRate = presenter.view.AskForNumber();
            }
            return newSweatRate;
        }

        /// <summary>
		/// Sets newMoveDescription
		/// </summary>
        private string GetNewMoveDescription()
        {
            presenter.view.AskForThis("description");
            return presenter.view.AskForString();
        }

        /// <summary>
		/// Creates new move
		/// </summary>
        public void AddNewMove()
        {
            string name = GetNewMoveName();
            int sweatRate = GetNewMoveSweatRate();
            string description = GetNewMoveDescription();
            crud.CreateMove(new Move()
            {
                Name = name,
                Description = description,
                SweatRate = sweatRate
            });
        }

        /// <summary>
		/// Sets SelectedMove based on chosenMoveId
		/// </summary>
        ///
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
		/// Sets userReview
		/// </summary>
        public void SetUserReview()
        {
            userReview = presenter.view.AskForUserReview();
            while (!(userReview >= 1 && userReview <= 5))
            {
                presenter.view.DisplayTryAgain("");
                userReview = presenter.view.AskForNumber();
            }
        }

        /// <summary>
		/// Sets userIntensity
		/// </summary>
        public void SetUserIntensity()
        {
            userIntensity = presenter.view.AskForUserIntensity();
            while (!(userIntensity >= 1 && userIntensity <= 5))
            {
                presenter.view.DisplayTryAgain("");
                userIntensity = presenter.view.AskForNumber();
            }
        }

    }
}

