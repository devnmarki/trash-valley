using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine;

public class RenderHelper
{
    public static Texture2D Pixel;

    public static void Init(GraphicsDevice graphics)
    {
        Pixel = new Texture2D(graphics, 1, 1);
        Pixel.SetData([Color.White]);
    }
}