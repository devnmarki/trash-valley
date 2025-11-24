using Microsoft.Xna.Framework;

namespace Engine;

public struct SpriteRenderer
{
    public Sprite? Sprite;
    public Color Color = Color.White;
    public float Layer = 0f;

    public SpriteRenderer()
    {
        Sprite = null;
        Color = Color.White;
        Layer = 0f;
    }
    
    public SpriteRenderer(Sprite? sprite)
    {
        Sprite = sprite;
        Color = Color.White;
        Layer = 0f;
    }
    
    public SpriteRenderer(Sprite? sprite, Color color, float layer = 0f)
    {
        Sprite = sprite;
        Color = color;
        Layer = layer;
    }
}