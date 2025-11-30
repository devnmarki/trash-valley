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
                if (player.State == PlayerState.Action) return;
                
                movement.Velocity = Input.GetVector("move_left", "move_right", "move_up", "move_down");

                player.State = IsMoving(movement) ? PlayerState.Move : PlayerState.Idle;

                if (Input.IsActionPressed("action"))
                {
                    Console.WriteLine($"Action!");
                }
                
                UpdateFacingDirection(ref player, movement);
            });
    }

    private void UpdateFacingDirection(ref PlayerComponent player, MovementComponent movement)
    {
        if (movement.Velocity.Length() == 0)
            return;
                
        if (Math.Abs(movement.Velocity.X) < Math.Abs(movement.Velocity.Y))
            player.Direction = movement.Velocity.Y > 0 ? Direction.Down : Direction.Up;
        else
            player.Direction = movement.Velocity.X > 0 ? Direction.Right : Direction.Left;
    }

    private void UpdateSpriteAnimation()
    {
        EntityQuery()
            .QueryAll<PlayerComponent, SpriteAnimator>()
            .ForEach((Entity entity, ref PlayerComponent player, ref SpriteAnimator animator) =>
            {
                string animationDir = player.Direction.ToString().ToLower();
                string animationState = player.State.ToString().ToLower();
                
                animator.PlayAnimation(animationState + "_" + animationDir);
            });
    }

    private bool IsMoving(MovementComponent movement)
    {
        return movement.Velocity != Vector2.Zero;
    }

    private bool InAction(PlayerComponent player)
    {
        return player.State == PlayerState.Action;
    }
}