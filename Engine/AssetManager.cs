using Engine.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

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

    public static void LoadTextures(string filename)
    {
        string json = File.ReadAllText("../../../" + filename);
        List<TextureData>? data = JsonConvert.DeserializeObject<List<TextureData>>(json);

        if (data == null) return;

        foreach (TextureData texture in data)
        {
            LoadTexture(texture.id, texture.path);
        }
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

    public static void LoadSpriteSheets(string filename)
    {
        string json = File.ReadAllText("../../../" + filename);
        List<SpriteSheetData>? data = JsonConvert.DeserializeObject<List<SpriteSheetData>>(json);

        if (data == null) return;

        foreach (SpriteSheetData sheet in data)
        {
            SpriteSheetProps sheetProps = new SpriteSheetProps()
            {
                Texture = GetTexture(sheet.texture_id)!,
                Columns = sheet.columns,
                Rows = sheet.rows,
                SpriteSize = new Vector2(sheet.sprite_width, sheet.sprite_height),
                Flip = sheet.flip
            };
            LoadSpriteSheet(sheet.id, sheetProps);
        }
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