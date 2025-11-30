using System;
using Engine;
using Microsoft.Xna.Framework;

namespace TrashValley;

public class DefaultScene : Scene
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        Console.WriteLine("Entered default scene!");
        
        AddSystem(new MovementSystem());
        AddSystem(new PlayerControllerSystem());
        
        AddEntity<PlayerEntity>();
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