using BornToMove.Business;
using BornToMove.DAL;

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
            // Startup application
            model.StartupMoves();
            view.DisplayWelcome();
            view.DisplayOpeningMessage();
            view.DisplayInitialOptions();

            // Asks user (1) generate move, or (2) choose move from list
            model.initialChoice = GetInitialChoice();

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
                    model.choiceFromList = GetChoiceFromList(model.moveNames);
                    model.nameChosenFromList = model.moveNames[model.choiceFromList];

                    if (model.choiceFromList == 0)
                    { // Asks user to enter new move
                        AddNewMove();
                    }
                    else
                    { // Sets selected move as chosen from list
                        model.SetSelectedMove(model.nameChosenFromList);
                    }
                }
            }
            if (model.selectedMove == null)
            { // Displays error if no moves selected
                view.DisplayGenericError();
            }
            else
            {   // Displays move and asks user rating and intensity
                view.DisplayMove(model.selectedMove);
                model.userRating = GetUserRating();
                model.userIntensity = GetUserIntensity();
            }
        }

        /// <summary>
        /// Sets initial choice
        /// </summary>
        /// <returns>An Integer, either 1 or 2</returns>
        public int GetInitialChoice()
        {
            int initialChoice = view.AskForNumber();
            while (!(model.ValidateInitialChoice(initialChoice)))
            {
                view.DisplayTryAgain("");
                initialChoice = view.AskForNumber();
            }
            return initialChoice;
        }

        /// <summary>
		/// Gets choice from list
		/// </summary>
        /// <param name="moveNames">A Dictionary of move IDs and move names</param>
        /// <returns>An Integer as key for move names list</returns>
        public int GetChoiceFromList(Dictionary<int, string> moveNames)
        {
            int choiceFromList = view.AskForNumber();
            while (!(moveNames.ContainsKey(choiceFromList) || choiceFromList == 0))
            {
                view.DisplayTryAgain("");
                choiceFromList = view.AskForNumber();
            }
            return choiceFromList;
            
        }

        /// <summary>
        /// Adds new move to DB
        /// </summary>
        public void AddNewMove()
        {
            string name = GetMoveName();
            int sweatRate = GetSweatRate();
            string description = GetDescription();
            model.SaveMove(new Move()
            {
                Name = name,
                SweatRate = sweatRate,
                Description = description
            });
        }

        /// <summary>
        /// Gets name for new move
        /// </summary>
        /// <returns>A String with move name</returns>
        private string GetMoveName()
        {
            view.AskForThis("move name");
            string name = view.AskForString();

            while (model.ValidateNewMoveName(name))
            {
                view.DisplayTryAgain("Move already exists. ");
                name = view.AskForString();
            }
            return name;
        }

        /// <summary>
        /// Gets sweatRate for new move
        /// </summary>
        /// <returns>An Integer between 1 and 5</returns>
        private int GetSweatRate()
        {
            view.AskForThis("sweatRate");
            int sweatRate = view.AskForNumber();
            while (!(model.ValidateNewMoveSweatRate(sweatRate)))
            {
                view.DisplayTryAgain("SweatRate should be between 1 and 5. ");
                sweatRate = view.AskForNumber();
            }
            return sweatRate;
        }

        /// <summary>
        /// Gets description for new move
        /// </summary>
        /// <returns>A string with new move description</returns>
        private string GetDescription()
        {
            view.AskForThis("description");
            return view.AskForString();
        }

        /// <summary>
        /// Gets review made by user
        /// </summary>
        /// <returns>An integer between 1 and 5</returns>
        public double GetUserRating()
        {
            double userReview = view.AskForUserRating();
            while (!(model.ValidateUserRating(userReview)))
            {
                view.DisplayTryAgain("");
                userReview = view.AskForNumber();
            }
            return userReview;
        }

        /// <summary>
		/// Gets intensity given by user
		/// </summary>
        /// <returns>An Integer between 1 and 5</returns>
        public double GetUserIntensity()
        {
            double userIntensity = view.AskForUserIntensity();
            while (!(model.ValidateUserIntensity(userIntensity)))
            {
                view.DisplayTryAgain("");
                userIntensity = view.AskForNumber();
            }
            return userIntensity;
        }
    }
}

