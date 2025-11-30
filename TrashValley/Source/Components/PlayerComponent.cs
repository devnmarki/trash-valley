namespace TrashValley;

public struct PlayerComponent
{
    public Direction Direction;
    public PlayerState AnimationState = PlayerState.Idle;

    public PlayerComponent() { }
}