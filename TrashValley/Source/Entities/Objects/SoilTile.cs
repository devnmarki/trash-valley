using Arch.Core;
using Engine;
using Microsoft.Xna.Framework;
using Entity = Engine.Entity;

namespace TrashValley;

public class SoilTile : Entity
{
    public SoilTile(World world, Arch.Core.Entity id) : base(world, id)
    {
        AddComponent(new SpriteRenderer()
        {
            Sprite = AssetManager.GetSpriteSheet("soil")!.GetSprite(0)
        });
    }
}