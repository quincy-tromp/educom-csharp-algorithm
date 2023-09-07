using BornToMove.DAL;

namespace BornToMove
{
    public class Model
	{
        // Properties
		private MoveCrud crud;
        public Presenter presenter;
		public Move? selectedMove; 
        public Dictionary<int, string> moveNames = new Dictionary<int, string>();
        public int choiceFromInitialOptions = -1;
        public string moveChosenFromList = "";
        public int fromListChoice = -1;
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
        public void SetInitialChoice()
        {
            choiceFromInitialOptions = presenter.view.AskForNumber();
            while (!(choiceFromInitialOptions == 1 || choiceFromInitialOptions == 2))
            {
                presenter.view.DisplayTryAgain("");
                choiceFromInitialOptions = presenter.view.AskForNumber();
            }
        }

        /// <summary>
		/// Sets moveChoice
		/// </summary>
        public void ChooseMoveFromList(Dictionary<int, string> moveNames)
        {
            fromListChoice = presenter.view.AskForNumber();
            while (!(moveNames.ContainsKey(fromListChoice) || fromListChoice == 0))
            {
                presenter.view.DisplayTryAgain("");
                fromListChoice = presenter.view.AskForNumber();
            }
            moveChosenFromList = moveNames[fromListChoice];
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
            crud.CreateMove(new Move() {
                Name = name,
                Description = description,
                SweatRate = sweatRate });
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

