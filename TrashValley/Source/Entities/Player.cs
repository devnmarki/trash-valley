using Arch.Core;
using Engine;
using Microsoft.Xna.Framework;
using Entity = Engine.Entity;

namespace TrashValley;

public class Player : Entity
{
    public Player(World world, Arch.Core.Entity id) : base(world, id)
    {
        ref var transform = ref GetComponent<Transform>();
        transform.Position = new Vector2(128);
        transform.Scale = new Vector2(3);
        
        InitializeComponents(transform);
        LoadAnimations();
    }

    private void InitializeComponents(Transform transform)
    {
        AddComponent(new SpriteRenderer()
        {
            Sprite = AssetManager.GetSpriteSheet("player")!.GetSprite(0),
        });
        AddComponent(new BoxCollider()
        {
            Size = new Vector2(16f) * transform.Scale
        });
        AddComponent(new Rigidbody()
        {
            Mass = 1,
            GravityScale = 0
        });
        AddComponent(new SpriteAnimator());
        AddComponent(new MovementComponent()
        {
            Velocity = new Vector2(0, 0),
            MoveSpeed = 400
        });
        AddComponent(new PlayerComponent());
    }
    
    private void LoadAnimations()
    {
        ref var animator = ref GetComponent<SpriteAnimator>();

        SpriteSheet sheet = AssetManager.GetSpriteSheet("player");
        if (sheet == null)
            return;
        
        animator.AddAnimation("idle_down", new SpriteAnimation(sheet, [0, 1, 2], 0.2f));
        animator.AddAnimation("idle_up", new SpriteAnimation(sheet, [4, 5, 6], 0.2f));
        animator.AddAnimation("idle_right", new SpriteAnimation(sheet, [8, 9, 10], 0.2f));
        animator.AddAnimation("idle_left", new SpriteAnimation(sheet, [8, 9, 10], 0.2f, true));
        
        animator.PlayAnimation("idle_down");
    }
}