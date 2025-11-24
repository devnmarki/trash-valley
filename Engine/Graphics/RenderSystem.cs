using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine;

public class RenderSystem : System
{
    public override void Update()
    {
        
    }

    public override void Render()
    {
        EntityQuery()
            .QueryAll<SpriteRenderer, Transform>()
            .ForEach((Entity entity, ref SpriteRenderer sr, ref Transform t) =>
            {
                Engine.Instance.SpriteBatch.Draw(
                    sr.Sprite?.Texture,
                    t.Position,
                    sr.Sprite?.SourceRectangle,
                    sr.Color,
                    t.Rotation,
                    Vector2.Zero,
                    t.Scale,
                    (bool)sr.Sprite?.Flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    sr.Layer / 1000f
                );
            });
    }
}