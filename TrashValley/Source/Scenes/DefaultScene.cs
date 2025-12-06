using System;
using Engine;

namespace TrashValley;

public class DefaultScene : Scene
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        Console.WriteLine("Entered default scene!");
        
        AddEntity<PlayerEntity>();
        AddEntity<Tool>(ModelDatabase.Tools.AxeModel);
        
        AddSystem(new MovementSystem());
        AddSystem(new PlayerControllerSystem());
        AddSystem(new ToolSystem());
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