using Microsoft.Xna.Framework;

namespace Engine;

public class PhysicsUtils
{
    public const float PixelsPerMeter = 16f;
    
    public static float ToMeters(float pixels) => pixels / PixelsPerMeter;
    public static Vector2 ToMeters(Vector2 pixels) => pixels / PixelsPerMeter;

    public static float ToPixels(float meters) => meters * PixelsPerMeter;
    public static Vector2 ToPixels(Vector2 meters) => meters * PixelsPerMeter;
}