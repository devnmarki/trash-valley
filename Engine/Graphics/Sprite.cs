using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine;

public struct Sprite
{
    public Texture2D? Texture = null;
    public Rectangle? SourceRectangle = null;
    public bool Flip = false;

    public Sprite()
    {
        
    }
}