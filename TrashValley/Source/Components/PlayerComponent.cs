using Microsoft.Xna.Framework;

namespace TrashValley;

public struct PlayerComponent
{
    public Direction Direction;
    public PlayerState State = PlayerState.Idle;
    public ToolModel? CurrentTool = null;
    
    public PlayerComponent() { }
}