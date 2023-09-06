using System;

namespace BornToMove
{
	public class View
	{	/// <summary>
		/// Displays welcome message
		/// </summary>
		public void DisplayWelcome()
		{
            Console.WriteLine("Welcome to BornToMove");
        }

        /// <summary>
        /// Displays opening message
        /// </summary>
        public void DisplayOpeningMessage()
		{
            Console.WriteLine("You Have To MOVE IT, MOVE IT!!");
        }

        /// <summary>
        /// Displays initial choices
        /// </summary>
        public void DisplayInitialChoice()
		{
            Console.WriteLine("\n");
            Console.WriteLine("Do you want to:");
			Console.WriteLine("1 get a move suggestion");
			Console.WriteLine("2 choose a move from a list?");
        }

        /// <summary>
		/// Read user input as Integer
		/// </summary>
		public int AskForNumber()
		{
            Console.Write(":");
            try
            {
                return Convert.ToInt16(Console.ReadLine());
            }
            catch
            {
                return AskForNumber();
            }
        }

        /// <summary>
		/// Read user input as String
		/// </summary>
        public string AskForString()
        {
            Console.Write(":");
            try
            {
                var input = Console.ReadLine();
                if (input != null)
                {
                    return input;
                }
                return AskForString();
            }
            catch
            {
                return AskForString();
            }
        }

        /// <summary>
		/// Displays move names
		/// </summary>
        /// <param name="moveNames">The associative array of move ids and move names from which to display move names</param>
        public void DisplayMoveNames(Dictionary<int, string> moveNames)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Choose one of the following moves: ");
            foreach (KeyValuePair<int, string> element in moveNames)
            {
                Console.WriteLine(element.Key + " " + element.Value);
            }
        }

        public void DisplayMove(Move move)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Move: " + move.Name);
            Console.WriteLine("SweatRate: " + move.SweatRate);
            Console.WriteLine("Description: " + move.Description);
        }

        public int AskForUserReview()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Rate your experience on a scale of 1 to 5");
            return AskForNumber();
        }

        public int AskForUserIntensity()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Rate the intensity on a scale of 1 to 5");
            return AskForNumber();
        }

        public void AskForThis(string askForThis)
        {
            Console.Write("Enter " + askForThis);
        }

        public void DisplayGenericError()
        {
            Console.WriteLine("Something went wrong, please try again later.");
        }

        public void DisplayCreatingNewMove()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Creating new move.");
        }

        public void DisplayTryAgain(string Error)
        {
            Console.WriteLine(Error + "Try again.");
        }
	}
}

