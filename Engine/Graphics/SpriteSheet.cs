using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine;

public struct SpriteSheetProps
{
    public Texture2D Texture;
    public int Rows;
    public int Columns;
    public Vector2 SpriteSize;
    public bool Flip;
}

public class SpriteSheet
{
    private readonly SpriteSheetProps _props;

    public List<Sprite> Sprites { get; } = [];
    
    public SpriteSheet(SpriteSheetProps props)
    {
        _props = props;
        
        Generate();
    }

    private void Generate()
    {
        for (int y = 0; y < _props.Rows; y++)
        {
            for (int x = 0; x < _props.Columns; x++)
            {
                Sprite sprite = new Sprite
                {
                    Texture = _props.Texture,
                    SourceRectangle = new Rectangle(
                        (int)(x * _props.SpriteSize.X), 
                        (int)(y * _props.SpriteSize.Y), 
                        (int)_props.SpriteSize.X, 
                        (int)_props.SpriteSize.Y
                    ),
                    Flip = _props.Flip
                };
                
                Sprites.Add(sprite);
            }
        }
    }

    public Sprite GetSprite(int index)
    {
        return Sprites[index];
    }
}