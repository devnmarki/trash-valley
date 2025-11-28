using System;
using Engine;
using Microsoft.Xna.Framework;

namespace TrashValley;

public class PlayerControllerSystem : Engine.System
{
    public override void Update()
    {
        HandleInputs();
        UpdateSpriteAnimation();
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
                
                if (movement.Velocity.Length() == 0)
                    return;
                
                if (Math.Abs(movement.Velocity.X) < Math.Abs(movement.Velocity.Y))
                    player.Direction = movement.Velocity.Y > 0 ? Direction.Down : Direction.Up;
                else
                    player.Direction = movement.Velocity.X > 0 ? Direction.Right : Direction.Left;
            });
    }

    private void UpdateSpriteAnimation()
    {
        EntityQuery()
            .QueryAll<PlayerComponent, SpriteAnimator>()
            .ForEach((Entity entity, ref PlayerComponent player, ref SpriteAnimator animator) =>
            {
                animator.PlayAnimation("idle" + "_" + player.Direction.ToString().ToLower());
            });
    }
}