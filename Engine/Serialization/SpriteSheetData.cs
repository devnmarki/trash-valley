namespace Engine;

public struct SpriteSheetData
{
    public string id { get; set; }
    public string texture_id { get; set; }
    public int columns { get; set; }
    public int rows { get; set; }
    public int sprite_width { get; set; }
    public int sprite_height { get; set; }
    public bool flip { get; set; }
}