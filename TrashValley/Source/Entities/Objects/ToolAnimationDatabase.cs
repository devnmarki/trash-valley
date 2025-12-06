using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TrashValley;

public struct ToolKeyFrame
{
    public int SpriteIndex;
    public Vector2 Offset;
    public float RenderingLayer;
}

public static class ToolAnimationDatabase
{
    public static readonly Dictionary<ToolType, Dictionary<Direction, ToolKeyFrame>> Frames = new()
    {
        {
            ToolType.Axe,
            new Dictionary<Direction, ToolKeyFrame>
            {
                {
                    Direction.Down,
                    new ToolKeyFrame()
                    {
                        SpriteIndex = 0,
                        Offset = new Vector2(2, 12),
                        RenderingLayer = Constants.Layers.Player - 1
                    }
                },
                {
                    Direction.Left,
                    new ToolKeyFrame()
                    {
                        SpriteIndex = 1,
                        Offset = new Vector2(-8, 4),
                        RenderingLayer = Constants.Layers.Player + 1
                    }
                },
                {
                    Direction.Up,
                    new ToolKeyFrame()
                    {
                        SpriteIndex = 2,
                        Offset = new Vector2(1, -8),
                        RenderingLayer = Constants.Layers.Player + 1
                    }
                },
                {
                    Direction.Right,
                    new ToolKeyFrame()
                    {
                        SpriteIndex = 3,
                        Offset = new Vector2(8, 4),
                        RenderingLayer = Constants.Layers.Player + 1
                    }
                },
            }
        }
    };
}