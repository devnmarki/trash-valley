using System;
using System.Numerics;
using Engine;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TrashValley;

public class PlayerControllerSystem : Engine.System
{
    private const float ActionDuration = 0.5f;
    
    private float _currentActionTime = 0f;
    private Vector2 _hitboxPosition = Vector2.Zero;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        
        UpdateHitbox();
        HandleInputs();
        UpdateSpriteAnimation();
    }

    public override void Render()
    {
        base.Render();

        DrawHitbox();
    }

    private void UpdateHitbox()
    {
        EntityQuery()
            .QueryAll<PlayerComponent>()
            .ForEach((Entity entity, ref PlayerComponent player) =>
            {
                float tileSize = Constants.TileSizeS;
                Vector2 hitboxOffset = Vector2.Zero;
                
                switch (player.Direction)
                {
                    case Direction.Up:
                        hitboxOffset.Y = -tileSize;
                        break;
                    case Direction.Down:
                        hitboxOffset.Y = tileSize;
                        break;
                    case Direction.Left:
                        hitboxOffset.X = -tileSize;
                        break;
                    case Direction.Right:
                        hitboxOffset.X = tileSize;
                        break;
                }
                
                Vector2 targetPosition = entity.GetComponent<Transform>().Position + hitboxOffset;

                _hitboxPosition = new Vector2(
                    MathF.Round(targetPosition.X / tileSize) * tileSize,
                    MathF.Round(targetPosition.Y / tileSize) * tileSize
                );
            });
    }

    private void DrawHitbox()
    {
        EntityQuery()
            .QueryAll<PlayerComponent, MovementComponent>()
            .ForEach((Entity entity, ref MovementComponent movement) =>
            {
                if (IsMoving(movement))
                    return;
                
                OrthographicCamera camera = SceneManager.ActiveScene?.SceneCamera;
                if (camera == null) 
                    return;

                Vector2 worldHitboxPosition = camera.ScreenToWorld(_hitboxPosition);
        
                Engine.Engine.Instance.SpriteBatch.DrawRectangle(
                    worldHitboxPosition.X, 
                    worldHitboxPosition.Y, 
                    Constants.TileSizeS, 
                    Constants.TileSizeS, 
                    Color.Black, 
                    1f, 
                    0.9f
                );
            });
    }

    private void HandleInputs()
    {
        EntityQuery()
            .QueryAll<PlayerComponent, MovementComponent>()
            .ForEach((Entity entity, ref PlayerComponent player, ref MovementComponent movement) =>
            {
                if (InAction(player))
                {
                    HandleActionState(ref player, ref movement);
                    return;
                }
                
                movement.Velocity = Input.GetVector("move_left", "move_right", "move_up", "move_down");
                
                if (Input.IsActionPressed("action"))
                {
                    player.State = PlayerState.Action;
                    _currentActionTime = ActionDuration;
                    return;
                }
                
                player.State = IsMoving(movement) ? PlayerState.Move : PlayerState.Idle;
                
                UpdateFacingDirection(ref player, movement);
            });
    }

    private void HandleActionState(ref PlayerComponent player, ref MovementComponent movement)
    {
        movement.Velocity = Vector2.Zero;
        _currentActionTime -= Time.DeltaTime;
                    
        if (_currentActionTime <= 0f)
            player.State = PlayerState.Idle;
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