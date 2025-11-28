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
        AddComponent(new MovementComponent()
        {
            Velocity = new Vector2(0, 0),
            MoveSpeed = 400
        });
        AddComponent(new PlayerComponent());
    }
}