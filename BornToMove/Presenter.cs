namespace BornToMove
{
    public class Presenter
	{
		public View view;
		private Model model;

		public Presenter(View view, Model model)
		{
			this.view = view;
			this.model = model;
            this.model.presenter = this;
		}

		public void HandleApp()
		{
			StartApp();
			RunApp();
		}

		private void StartApp()
		{
			view.DisplayWelcome();
			view.DisplayOpeningMessage();
        }

        private void RunApp()
        {
            view.DisplayInitialChoice();
            model.GetUserChoice();

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

                    if (model.MoveChoice != -1)
                    {
                        if (model.MoveChoice == 0)
                        {
                            AddNewMove();
                        }
                        else
                        {
                            model.SetMove(model.MoveChoice);
                        }
                    }
                    else
                    {
                        view.DisplayGenericError();
                    }
                }
            }

            if (model.move == null)
            {
                view.DisplayGenericError();
            }
            else
            {
                view.DisplayMove(model.move);
                GetUserReview();
                GetUserIntensity();
            }
        }

		private void AddNewMove()
		{
            view.DisplayCreatingNewMove();
            string moveName = GetNewMoveName(model.moveNames);
            int sweatRate = GetNewMoveSweatRate();
            string description = GetNewMoveDescription();
            model.AddOneMove(moveName, sweatRate, description);
        }

		private string GetNewMoveName(Dictionary<int, string> moveNames)
		{
			view.AskForThis("move name");
            string moveName = view.AskForString();

            while (moveNames.ContainsValue(moveName))
            {
				view.DisplayTryAgain("Move already exists. ");
                moveName = view.AskForString();
            }
			return moveName;
        }

		private int GetNewMoveSweatRate()
		{
			view.AskForThis("sweatRate");
            int sweatRate = view.AskForNumber();
            while (!(sweatRate >= 1 && sweatRate <= 5))
            {
				view.DisplayTryAgain("SweatRate should be between 1 and 5. ");
                sweatRate = view.AskForNumber();
            }
			return sweatRate;
        }

		private string GetNewMoveDescription()
		{
			view.AskForThis("description");
			return view.AskForString();
		}

		public int GetUserReview()
		{
            int userReview = view.AskForUserReview();
            while (!(userReview >= 1 && userReview <= 5))
            {
				view.DisplayTryAgain("");
                userReview = view.AskForNumber();
            }
			return userReview;
        }

		public int GetUserIntensity()
		{
            int userIntensity = view.AskForUserIntensity();
            while (!(userIntensity >= 1 && userIntensity <= 5))
            {
				view.DisplayTryAgain("");
                userIntensity = view.AskForNumber();
            }
			return userIntensity;
        }
    }
}

