using System;
using BornToMove.DAL;

namespace BornToMove.Business
{
	interface IView
	{
        void DisplayWelcome();

        void DisplayOpeningMessage();

        void DisplayInitialOptions();

        int AskForNumber();

        string AskForString();

        void DisplayMoveNames(Dictionary<int, string> moveNames);

        void DisplayMove(Move move);

        int AskForUserReview();

        int AskForUserIntensity();

        void AskForThis(string askForThis);

        void DisplayGenericError();

        void DisplayCreatingNewMove();

        void DisplayTryAgain(string Error);
    }
}

