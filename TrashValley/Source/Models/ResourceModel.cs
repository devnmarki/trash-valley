using Engine;

namespace TrashValley;

public record struct ResourceModel
{
    public string Name;
    public Sprite Sprite;
    public float MaxHealth;
    
    public ResourceModel(string name, Sprite sprite, float maxHealth)
    {
        Name = name;
        Sprite = sprite;
        MaxHealth = maxHealth;
    }
}