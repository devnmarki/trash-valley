using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine;

public class RenderSystem : System
{
    public override void Update()
    {
        EntityQuery()
            .QueryAll<SpriteAnimator>()
            .ForEach((Entity entity, ref SpriteAnimator animator) =>
            {
                animator.CurrentAnimation?.Tick();
            });
    }

    public override void Render()
    {
        EntityQuery()
            .QueryAll<SpriteRenderer, Transform>()
            .Exclude<SpriteAnimator>()
            .ForEach((Entity entity, ref SpriteRenderer sr, ref Transform t) =>
            {
                if (sr.Sprite?.Texture == null) return;
                
                Engine.Instance.SpriteBatch.Draw(
                    sr.Sprite?.Texture,
                    t.Position,
                    sr.Sprite?.SourceRectangle,
                    sr.Color,
                    t.Rotation,
                    new Vector2((float)sr.Sprite?.SourceRectangle?.Width! / 2f, (float)sr.Sprite?.SourceRectangle?.Height! / 2f),
                    t.Scale,
                    (bool)sr.Sprite?.Flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    sr.Layer / 1000f
                );
            });
        
        EntityQuery()
            .QueryAll<SpriteAnimator, SpriteRenderer, Transform>()
            .ForEach((Entity entity, ref SpriteAnimator animator, ref SpriteRenderer sr, ref Transform t) =>
            {
                if (animator.CurrentAnimation == null) 
                    return;

                SpriteAnimation? currentAnim = animator.CurrentAnimation;
                
                Sprite currentSprite = currentAnim.SpriteSheet.GetSprite(currentAnim.Frames[currentAnim.CurrentFrameIndex]);
                currentSprite.Flip = currentAnim.Flip;

                sr.Sprite = currentSprite;
                
                Engine.Instance.SpriteBatch.Draw(
                    sr.Sprite?.Texture,
                    t.Position,
                    sr.Sprite?.SourceRectangle,
                    sr.Color,
                    t.Rotation,
                    new Vector2((float)sr.Sprite?.SourceRectangle?.Width! / 2f, (float)sr.Sprite?.SourceRectangle?.Height! / 2f),
                    t.Scale,
                    (bool)sr.Sprite?.Flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    sr.Layer / 1000f
                );
            });
    }
}