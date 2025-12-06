using Arch.Core;
using Engine;
using Microsoft.Xna.Framework;
using Entity = Engine.Entity;

namespace TrashValley;

public class Tool : Entity
{
    public Tool(World world, Arch.Core.Entity id, ToolModel model) : base(world, id)
    {
        ToolComponent toolComponent = AddComponent<ToolComponent>();
        toolComponent.Model = model;
        SpriteSheet spriteSheet = toolComponent.Model.SpriteSheet;

        AddComponent(new SpriteRenderer()
        {
            Sprite = spriteSheet.GetSprite(0)
        });
    }
}