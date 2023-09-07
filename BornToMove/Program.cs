using System;
using BornToMove.DAL;

namespace BornToMove;

class BornToMove
{
    static void Main(string[] args)
    {
        MoveContext context = new MoveContext();
        MoveCrud moveCrud = new MoveCrud(context);
        Model model = new Model(moveCrud);
        View view = new View();
        Presenter presenter = new Presenter(view, model);
        presenter.RunApp();

    }
}