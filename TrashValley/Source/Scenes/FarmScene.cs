using System;
using Engine;

namespace TrashValley;

public class FarmScene : Scene
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        Console.WriteLine("Entered farm scene!");
    }

    public override void OnExit()
    {
        base.OnExit();
        
        Console.WriteLine("Exited farm scene!");
    }
}