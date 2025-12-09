using Arch.Core;
using Engine;
using Microsoft.Xna.Framework;
using Entity = Engine.Entity;

namespace TrashValley;

public class PlayerEntity : Entity
{
    private const float MoveSpeed = 550f;
    
    public PlayerEntity(World world, Arch.Core.Entity id) : base(world, id)
    {
        ref var transform = ref GetComponent<Transform>();
        transform.Position = new Vector2(128);
        transform.Scale = new Vector2(Constants.ScaleFactor);
        
        InitializeComponents(transform);
        LoadAnimations();
    }

    private void InitializeComponents(Transform transform)
    {
        AddComponent(new SpriteRenderer()
        {
            Sprite = AssetManager.GetSpriteSheet("player")!.GetSprite(0),
            Layer = Constants.Layers.Player,
            YSort = true
        });
        AddComponent(new SpriteAnimator());
        AddComponent(new BoxCollider()
        {
            Size = new Vector2(16f) * transform.Scale
        });
        AddComponent(new Rigidbody()
        {
            Mass = 1,
            GravityScale = 0
        });
        AddComponent(new MovementComponent()
        {
            Velocity = new Vector2(0, 0),
            MoveSpeed = MoveSpeed
        });
        AddComponent(new PlayerComponent()
        {
            Direction = Direction.Down
        });
    }
    
    private void LoadAnimations()
    {
        ref var animator = ref GetComponent<SpriteAnimator>();

        SpriteSheet sheet = AssetManager.GetSpriteSheet("player");
        if (sheet == null)
            return;
        
        animator.LoadAnimations("Data/Assets/Animations/player_animations.json");
        animator.PlayAnimation("idle_down");
    }
}