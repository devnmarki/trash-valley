using Microsoft.Xna.Framework.Graphics;

namespace Engine;

public class AssetManager
{
    private static readonly Dictionary<string, Texture2D> Textures = new();
    private static readonly Dictionary<string, SpriteSheet> SpriteSheets = new();
    
    public static Texture2D? LoadTexture(string id, string path)
    {
        Texture2D texture = Engine.Instance.Content.Load<Texture2D>(path);
        bool valid = Textures.TryAdd(id, texture);

        if (valid) 
            return texture;
        
        Console.WriteLine($"Texture {id} already exists!");
        return null;
    }

    public static Texture2D? GetTexture(string id)
    {
        bool valid = Textures.TryGetValue(id, out var target);
        if (valid) 
            return target;
        
        Console.WriteLine($"Unknown texture '{id}'.");
        return null;
    }

    public static SpriteSheet? LoadSpriteSheet(string id, SpriteSheetProps props)
    {
        SpriteSheet sheet = new SpriteSheet(props);
        bool valid = SpriteSheets.TryAdd(id, sheet);
        
        if (valid) 
            return sheet;
        
        Console.WriteLine($"Sprite Sheet {id} already exists!");
        return null;
    }
    
    public static SpriteSheet? GetSpriteSheet(string id)
    {
        bool valid = SpriteSheets.TryGetValue(id, out var target);
        if (valid) 
            return target;
        
        Console.WriteLine($"Unknown sprite sheet '{id}'.");
        return null;
    }
}