using System;
using MySql.Data.MySqlClient;

namespace BornToMove;

class BornToMove
{
    static void Main(string[] args)
    {
        //Console.WriteLine("You have to move it, move  it! You have to MOVE IT!!\n");
        //getUserMovement();
        Crud crud = new Crud();
        crud.createMovement("Push Up", "Ga horizontaal liggen op teentoppen en handen. Laat het lijf langzaam zakken tot de neus de grond bijna raakt. Duw het lijf terug nu omhoog tot de ellebogen bijna gestrekt zijn. Vervolgens weer laten zakken. Doe dit 20 keer zonder tussenpauzes", 3);
        //Dictionary<int, Move> moves = crud.getMovements();

    }

    private static int getUserMovement()
    {
        //string userMovement;

        Console.WriteLine("Do you want: \n(1) get a movement suggestion, or \n(2) choose a movement from list?");
        Console.Write(": ");
        int userChoice = Convert.ToInt16(Console.ReadLine());
        if (userChoice == 1)
        {

        }
        else if (userChoice == 2)
        {
            
        }
        return userChoice;
    }
}

