using System;
using BornToMove.Business;
using BornToMove.DAL;

namespace BornToMove;

class BornToMove
{
    static void Main(string[] args)
    {
        MoveContext context = new MoveContext();
        MoveCrud moveCrud = new MoveCrud(context);
        BuMove model = new BuMove(moveCrud);
        View view = new View();
        Presenter presenter = new Presenter(view, model);
        presenter.RunApp();

    }
}