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
        transform.Position = new Vector2(200, 200);
        transform.Scale = new Vector2(3, 3);
        
        AddComponent(new SpriteRenderer()
        {
            Sprite = AssetManager.GetSpriteSheet("player_spritesheet")!.GetSprite(0),
        });
    }
}