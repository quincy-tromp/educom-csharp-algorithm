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
        /// Handles application 
        /// </summary>
		public void HandleApp()
		{
			StartApp();
			RunApp();
		}

        /// <summary>
        /// Starts up application
        /// </summary>
		private void StartApp()
		{
			view.DisplayWelcome();
			view.DisplayOpeningMessage();
        }

        /// <summary>
        /// Runs the application logic
        /// </summary>
        private void RunApp()
        {
            view.DisplayInitialChoice();
            model.SetUserChoice();

            if (model.userChoice == 1)
            {
                model.GenerateMoveSuggestion();
            }
			if (model.userChoice == 2)
			{
				model.SetMoveNames();

                if (model.moveNames == null)
                {
                    view.DisplayGenericError();
                }
                else
                {
                    view.DisplayMoveNames(model.moveNames);
                    model.ChooseMoveFromList(model.moveNames);

                    if (model.MoveChoice == -1)
                    {
                        view.DisplayGenericError();
                    }
                    else
                    {
                        if (model.MoveChoice == 0)
                        {
                            model.AddNewMove();
                        }
                        else
                        {
                            model.SetChosenMove(model.MoveChoice);
                        }
                    }
                }
            }
            if (model.chosenMove == null)
            {
                view.DisplayGenericError();
            }
            else
            {
                view.DisplayMove(model.chosenMove);
                model.SetUserReview();
                model.SetUserIntensity();
            }
        }
    }
}

