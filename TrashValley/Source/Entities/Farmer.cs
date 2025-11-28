using Arch.Core;
using Engine;
using Microsoft.Xna.Framework;
using Entity = Engine.Entity;

namespace TrashValley;

public class Farmer : Entity
{
    public Farmer(World world, Arch.Core.Entity id) : base(world, id)
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
    }
}