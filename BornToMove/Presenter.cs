using BornToMove.DAL;
using BornToMove.Business;

namespace BornToMove
{
    public class Presenter
	{
        // Fields
		private View view;
        private BuMove model;

        // Constructor
		public Presenter(View view, BuMove model)
		{
			this.view = view;
			this.model = model;
		}

        /// <summary>
        /// Contains the application logic
        /// </summary>
        public void RunApp()
        {
            // Start application
            view.DisplayWelcome();
            view.DisplayOpeningMessage();
            view.DisplayInitialOptions();
            // Asks user (1) generate move, or (2) choose move from list
            SetInitialChoice();

            if (model.initialChoice == 1)
            { // Generates a move
                model.GenerateMoveSuggestion();
            }
			if (model.initialChoice == 2)
			{ // Gets list of move names
				model.GetMoveNameList();
                
                if (model.moveNames == null)
                { // Displays error if no move names retrieved
                    view.DisplayGenericError();
                }
                else
                { // Display list of move names and asks user to choose one
                    view.DisplayMoveNames(model.moveNames);
                    ChooseMoveFromList(model.moveNames);

                    if (model.choiceFromList == 0)
                    { // Asks user to enter new move
                        AddNewMove();
                    }
                    else
                    { // Sets selected move as chosen from list
                        model.SetSelectedMove(model.nameOfMoveChosenFromList);
                    }
                }
            }
            if (model.selectedMove == null)
            { // Displays error if no moves selected
                view.DisplayGenericError();
            }
            else
            {   // Displays move and asks user review and intensity
                view.DisplayMove(model.selectedMove);
                SetUserReview();
                SetUserIntensity();
            }
        }

        /// <summary>
        /// Sets initial choice
        /// </summary>
        public void SetInitialChoice()
        {
            int initialChoice = view.AskForNumber();
            while (!(model.ValidateInitialChoice(initialChoice)))
            {
                view.DisplayTryAgain("");
                initialChoice = view.AskForNumber();
            }
            model.initialChoice = initialChoice;
        }

        /// <summary>
		/// Sets choiceFromList and nameOfMoveChosenFromList
		/// </summary>
        /// <param name="moveNames">A Dictionary of move IDs and move names</param>
        public void ChooseMoveFromList(Dictionary<int, string> moveNames)
        {
            int choiceFromList = view.AskForNumber();
            while (!(moveNames.ContainsKey(choiceFromList) || choiceFromList == 0))
            {
                view.DisplayTryAgain("");
                choiceFromList = view.AskForNumber();
            }
            model.choiceFromList = choiceFromList;
            model.nameOfMoveChosenFromList = moveNames[choiceFromList];
        }

        public void AddNewMove()
        {
            string name = GetNewMoveName();
            int sweatRate = GetNewMoveSweatRate();
            string description = GetNewMoveDescription();
            model.CreateNewMove(new Move()
            {
                Name = name,
                SweatRate = sweatRate,
                Description = description
            });
        }

        /// <summary>
        /// Gets new move name
        /// </summary>
        /// <returns>A String move name</returns>
        private string GetNewMoveName()
        {
            view.AskForThis("move name");
            string newName = view.AskForString();

            while (model.ValidateNewMoveName(newName))
            {
                view.DisplayTryAgain("Move already exists. ");
                newName = view.AskForString();
            }
            return newName;
        }

        /// <summary>
        /// Gets new move sweatRate
        /// </summary>
        /// <returns>A Integer sweatRate</returns>
        private int GetNewMoveSweatRate()
        {
            view.AskForThis("sweatRate");
            int newSweatRate = view.AskForNumber();
            while (!(model.ValidateNewMoveSweatRate(newSweatRate)))
            {
                view.DisplayTryAgain("SweatRate should be between 1 and 5. ");
                newSweatRate = view.AskForNumber();
            }
            return newSweatRate;
        }

        /// <summary>
        /// Sets newMoveDescription
        /// </summary>
        private string GetNewMoveDescription()
        {
            view.AskForThis("description");
            return view.AskForString();
        }

        /// <summary>
        /// Sets userReview
        /// </summary>
        public void SetUserReview()
        {
            int userReview = view.AskForUserReview();
            while (!(model.ValidateUserReview(userReview)))
            {
                view.DisplayTryAgain("");
                userReview = view.AskForNumber();
            }
            model.userReview = userReview;
        }

        /// <summary>
		/// Sets userIntensity
		/// </summary>
        public void SetUserIntensity()
        {
            int userIntensity = view.AskForUserIntensity();
            while (!(model.ValidateUserIntensity(userIntensity)))
            {
                view.DisplayTryAgain("");
                userIntensity = view.AskForNumber();
            }
            model.userIntensity = userIntensity;
        }
    }
}

