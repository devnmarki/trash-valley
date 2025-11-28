using Engine;
using Microsoft.Xna.Framework;

namespace TrashValley;

public class PlayerControllerSystem : Engine.System
{
    public override void Update()
    {
        HandleInputs();
    }

    public override void Render()
    {
        
    }

    private void HandleInputs()
    {
        EntityQuery()
            .QueryAll<PlayerComponent, MovementComponent>()
            .ForEach((Entity entity, ref PlayerComponent player, ref MovementComponent movement) =>
            {
                movement.Velocity = Input.GetVector("move_left", "move_right", "move_up", "move_down");
            });
    }
}