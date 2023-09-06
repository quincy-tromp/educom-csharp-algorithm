using System;
using System.Xml.Linq;

namespace BornToMove
{
	public class Model
	{
        // Properties
		private MoveCrud crud;
        public Presenter presenter;
		public Move? chosenMove; 
        public Dictionary<int, string>? moveNames;
        public int userChoice = -1;
        public int MoveChoice = -1; 
        public string newMoveName = "";
        public int newMoveSweatRate = -1;
        public string newMoveDescription = "";
        public int userReview = -1;
        public int userIntensity = -1;

        // Constructor
		public Model(MoveCrud crud)
		{
			this.crud = crud;
		}

        /// <summary>
		/// Sets userChoice
		/// </summary>
        public void SetUserChoice()
        {
            userChoice = presenter.view.AskForNumber();
            while (!(userChoice == 1 || userChoice == 2))
            {
                presenter.view.DisplayTryAgain("");
                userChoice = presenter.view.AskForNumber();
            }
        }

        /// <summary>
		/// Sets moveChoice
		/// </summary>
        public void ChooseMoveFromList(Dictionary<int, string> moveNames)
        {
            MoveChoice = presenter.view.AskForNumber();
            while (!(moveNames.ContainsKey(MoveChoice) || MoveChoice == 0))
            {
                presenter.view.DisplayTryAgain("");
                MoveChoice = presenter.view.AskForNumber();
            }
        }

        /// <summary>
		/// Generates a move suggestion for user/Sets chosenMove
		/// </summary>
        public void GenerateMoveSuggestion()
        {
            var moveIds = crud.GetAllMoveIds();
            if (moveIds != null)
            {
                Random random = new Random();
                chosenMove = crud.GetMoveById(random.Next(1, moveIds.Count));
            }
        }

        /// <summary>
		/// Sets moveNames
		/// </summary>
        public void SetMoveNames()
        {
            moveNames = crud.GetMoveNames();
        }

        /// <summary>
		/// Sets newMoveName
		/// </summary>
        private void SetNewMoveName()
        {
            presenter.view.AskForThis("move name");
            newMoveName = presenter.view.AskForString();

            while (moveNames.ContainsValue(newMoveName))
            {
                presenter.view.DisplayTryAgain("Move already exists. ");
                newMoveName = presenter.view.AskForString();
            }
        }

        /// <summary>
		/// Sets newMoveSweatRate
		/// </summary>
        private void SetNewMoveSweatRate()
        {
            presenter.view.AskForThis("sweatRate");
            newMoveSweatRate = presenter.view.AskForNumber();
            while (!(newMoveSweatRate >= 1 && newMoveSweatRate <= 5))
            {
                presenter.view.DisplayTryAgain("SweatRate should be between 1 and 5. ");
                newMoveSweatRate = presenter.view.AskForNumber();
            }
        }

        /// <summary>
		/// Sets newMoveDescription
		/// </summary>
        private void SetNewMoveDescription()
        {
            presenter.view.AskForThis("description");
            newMoveDescription = presenter.view.AskForString();
        }

        /// <summary>
		/// Creates new move
		/// </summary>
        public void AddNewMove()
        {
            SetNewMoveName();
            SetNewMoveSweatRate();
            SetNewMoveDescription();
            crud.CreateOneMove(newMoveName, newMoveSweatRate, newMoveDescription);
        }

        /// <summary>
		/// Sets chosenMove based on chosenMoveId
		/// </summary>
        ///
        ///<param name="chosenMoveId">The ID of the chosen move</param>
        public void SetChosenMove(int chosenMoveId)
        {
            try
            {
                chosenMove = crud.GetMoveById(chosenMoveId);
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

