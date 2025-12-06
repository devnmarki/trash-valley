using Engine;

namespace TrashValley;

public record struct ToolModel
{
    public readonly string Name;
    public readonly ToolType Type;
    public readonly SpriteSheet SpriteSheet;
    public readonly int Damage;

    public ToolModel(string name, ToolType type, SpriteSheet spriteSheet, int damage = 1)
    {
        Name = name;
        Type = type;
        SpriteSheet = spriteSheet;
        Damage = damage;
    }
}