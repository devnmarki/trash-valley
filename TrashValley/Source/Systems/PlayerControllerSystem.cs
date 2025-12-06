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
                Vector2 mousePosition = Input.GetMouseScreenPosition();
                float tileSize = Constants.TileSizeS;

                _hitboxPosition = new Vector2(
                    MathF.Round((mousePosition.X - tileSize / 2) / tileSize) * tileSize,
                    MathF.Round((mousePosition.Y - tileSize / 2) / tileSize) * tileSize
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
                Vector2 playerPosition = entity.Transform.Position;

                if (!InPlayerRange(playerPosition, worldHitboxPosition))
                    return;
        
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

                    if (player.CurrentTool == null)
                    {
                        player.CurrentTool = ModelDatabase.Tools.AxeModel;
                        SceneManager.ActiveScene?.AddEntity<ToolEntity>(player.CurrentTool);
                    }
                    
                    return;
                }
                
                movement.Velocity = Input.GetVector("move_left", "move_right", "move_up", "move_down");
                
                if (Input.IsActionPressed("action"))
                {
                    player.State = PlayerState.Action;
                    
                    UpdateFacingDirectionDuringAction(entity, ref player);
                    
                    _currentActionTime = ActionDuration;
                    return;
                }
                
                player.State = IsMoving(movement) ? PlayerState.Move : PlayerState.Idle;
                
                UpdateFacingDirection(entity, ref player, movement);
            });
    }

    private void HandleActionState(ref PlayerComponent player, ref MovementComponent movement)
    {
        movement.Velocity = Vector2.Zero;
        _currentActionTime -= Time.DeltaTime;
                    
        if (_currentActionTime <= 0f)
            player.State = PlayerState.Idle;
    }

    private void UpdateFacingDirection(Entity playerEntity, ref PlayerComponent player, MovementComponent movement)
    {
        if (movement.Velocity.Length() == 0)
            return;
                
        if (Math.Abs(movement.Velocity.X) < Math.Abs(movement.Velocity.Y))
            player.Direction = movement.Velocity.Y > 0 ? Direction.Down : Direction.Up;
        else
            player.Direction = movement.Velocity.X > 0 ? Direction.Right : Direction.Left;
    }

    private void UpdateFacingDirectionDuringAction(Entity playerEntity, ref PlayerComponent player)
    {
        Vector2 playerPos = playerEntity.Transform.Position;
        if (!InPlayerRange(playerPos, _hitboxPosition)) 
            return;
            
        Vector2 diff = _hitboxPosition - playerPos;
        if (Math.Abs(diff.X) > Math.Abs(diff.Y))
            player.Direction = diff.X < 0 ? Direction.Left : Direction.Right;
        else
            player.Direction = diff.Y < 0 ? Direction.Up : Direction.Down;
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

    private bool InPlayerRange(Vector2 playerPosition, Vector2 currentPosition)
    {
        const float tileSize = Constants.TileSizeS;
        return !(currentPosition.X > playerPosition.X + tileSize || 
                 currentPosition.X < playerPosition.X - tileSize * 2 || 
                 currentPosition.Y > playerPosition.Y + tileSize ||
                 currentPosition.Y < playerPosition.Y - tileSize * 2);
    }
}