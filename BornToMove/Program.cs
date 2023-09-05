using System;
using MySql.Data.MySqlClient;

namespace BornToMove;

class BornToMove
{
    static void Main(string[] args)
    {
        Console.WriteLine("You have to move it, move  it! You have to MOVE IT!!\n");
        Move userMove = GetUserMove();
    }

    //private static void CreateInitialMoves()
    //{
    //    Crud crud = new Crud();
    //    crud.CreateMove("Push Up", "Ga horizontaal liggen op teentoppen en handen. Laat het lijf langzaam zakken tot de neus de grond bijna raakt. Duw het lijf terug nu omhoog tot de ellebogen bijna gestrekt zijn. Vervolgens weer laten zakken. Doe dit 20 keer zonder tussenpauzes", 3);
    //    crud.CreateMove("Planking", "Ga horizontaal liggen op teentoppen en onderarmen. Houdt deze positie 1 minuut vast", 3);
    //    crud.CreateMove("Squat", "Ga staan met gestrekte armen. Zak door de knieën tot de billen de grond bijna raken. Ga weer volledig gestrekt staan. Herhaal dit 20 keer zonder tussenpauzes", 5);
    //}

    private static Move GetUserMove()
    {
        int userChoice = ReadUserChoice();
        Move userMove = ProcessUserChoice(userChoice);
        return userMove;

    }

    private static void CreateNewMove()
    {
        ///
    }

    private static int ReadUserChoice()
    {
        Console.WriteLine("Do you want to: \n(1) get a move suggestion \n(2) choose a move from the list?");
        Console.Write(": ");
        int userChoice = Convert.ToInt16(Console.ReadLine());
        while (!(userChoice == 1 || userChoice == 2))
        {
            Console.WriteLine("Try again.");
            userChoice = ReadUserChoice();
        }
        return userChoice;
    }

    private static Move ProcessUserChoice(int userChoice)
    {
        Crud crud = new Crud();
        if (userChoice == 1)
        {
            List<int> moveIds = crud.ReadMoveIds();
            Random random = new Random();
            int moveId = random.Next(1, moveIds.Count);
            Move move = crud.ReadMoveById(moveId);
            return move;
        }
        else
        {
            Dictionary<int, string> moveNames = crud.ReadAllMoveNames();
            int userMoveChoice = ReadUserMoveChoice(moveNames);
            Move move = crud.ReadMoveById(userMoveChoice);
            return move;
        }
    }

    private static int ReadUserMoveChoice(Dictionary<int, string> moveNames)
    {
        Console.WriteLine("Choose one of the following moves: ");
        foreach (KeyValuePair<int, string> element in moveNames)
        {
            Console.WriteLine(element.Key + " " + element.Value);
        }
        int userMoveChoice = Convert.ToInt16(Console.ReadLine());
        while (!moveNames.ContainsKey(userMoveChoice))
        {
            userMoveChoice = ReadUserMoveChoice(moveNames);
        }
        return userMoveChoice;
    }
}

