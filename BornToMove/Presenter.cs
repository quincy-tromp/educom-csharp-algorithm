using BornToMove.DAL;
using BornToMove.Business;

namespace BornToMove
{
    public class Presenter : IPresenter
	{
        // Properties
		public View view;
		private BuMove model;

        // Constructor
		public Presenter(View view, BuMove model)
		{
			this.view = view;
			this.model = model;
            this.model.presenter = this;
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
            model.SetInitialChoice();

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
                    model.ChooseMoveFromList(model.moveNames);

                    if (model.choiceFromList == 0)
                    { // Asks user to enter new move
                        model.AddNewMove();
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
                model.SetUserReview();
                model.SetUserIntensity();
            }
        }
    }
}

