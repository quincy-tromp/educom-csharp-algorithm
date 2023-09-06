using System;
using MySql.Data.MySqlClient;

namespace BornToMove;

class BornToMove
{
    static void Main(string[] args)
    {
        Crud crud = new Crud();
        MoveCrud moveCrud = new MoveCrud(crud);
        Model model = new Model(moveCrud);
        View view = new View();
        Presenter presenter = new Presenter(view, model);
        presenter.HandleApp();

    }
}