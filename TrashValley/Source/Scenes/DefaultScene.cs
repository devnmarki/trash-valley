using System;
using Engine;

namespace TrashValley;

public class DefaultScene : Scene
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        Console.WriteLine("Entered default scene!");
        
        AddEntity<Farmer>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        
        Console.WriteLine("Exited default scene!");
    }
}