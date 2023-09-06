using System;
using System.Xml.Linq;

namespace BornToMove
{
	public class Model
	{
		private MoveCrud crud;
        public Presenter presenter;
		public Move? move;
        public Dictionary<int, string>? moveNames;
        public int userChoice;
        public int MoveChoice;

		public Model(MoveCrud crud)
		{
			this.crud = crud;
		}

        public void GetUserChoice()
        {
            userChoice = presenter.view.AskForNumber();
            while (!(userChoice == 1 || userChoice == 2))
            {
                presenter.view.DisplayTryAgain("");
                userChoice = presenter.view.AskForNumber();
            }
        }

        public void ChooseMoveFromList(Dictionary<int, string> moveNames)
        {
            MoveChoice = presenter.view.AskForNumber();
            while (!(moveNames.ContainsKey(MoveChoice) || MoveChoice == 0))
            {
                presenter.view.DisplayTryAgain("");
                MoveChoice = presenter.view.AskForNumber();
            }
        }

        public void GenerateMoveSuggestion()
        {
            var moveIds = crud.GetAllMoveIds();
            if (moveIds != null)
            {
                Random random = new Random();
                move = crud.GetMoveById(random.Next(1, moveIds.Count));
            }
        }

        public void SetMoveNames()
        {
            moveNames = crud.GetMoveNames();
        }

        public void AddOneMove(string name, int sweatRate, string description)
        {
            crud.CreateOneMove(name, sweatRate, description);
        }

        public void SetMove(int chosenMoveId)
        {
            try
            {
                move = crud.GetMoveById(chosenMoveId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Process failed due to technical error: " + e.Message);
            }
        }
    }
}

