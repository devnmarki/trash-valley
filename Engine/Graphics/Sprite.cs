using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine;

public struct Sprite
{
    public Texture2D? Texture { get; set; }
    public Rectangle? SourceRectangle { get; set; }
    public bool Flip { get; set; }
}