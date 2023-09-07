using BornToMove.DAL;

namespace BornToMove
{
    public class Presenter
	{
        // Properties
		public View view;
		private Model model;

        // Constructor
		public Presenter(View view, Model model)
		{
			this.view = view;
			this.model = model;
            this.model.presenter = this;
		}

        /// <summary>
        /// Runs the application logic
        /// </summary>
        public void RunApp()
        {
            // Start application
            view.DisplayWelcome();
            view.DisplayOpeningMessage();
            view.DisplayInitialOptions();
            // Asks user (1) generate move, or (2) choose move from list
            model.SetInitialChoice();

            if (model.choiceFromInitialOptions == 1)
            { // Generates a move
                model.GenerateMoveSuggestion();
            }
			if (model.choiceFromInitialOptions == 2)
			{ // Gets list of move names
				model.GetMoveNameList();
                
                if (model.moveNames == null)
                { // Displays error if no move names retrieved
                    view.DisplayGenericError();
                }
                else
                { // Display list of move names and asks user to choose one
                    view.DisplayMoveNames(model.moveNames);
                    model.ChooseMoveFromList(model.moveNames);

                    if (model.fromListChoice == 0)
                    { // Asks user to enter new move
                        model.AddNewMove();
                    }
                    else
                    { // Sets selected move as chosen from list
                        model.SetSelectedMove(model.moveChosenFromList);
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
                model.SetUserReview();
                model.SetUserIntensity();
            }
        }
    }
}

