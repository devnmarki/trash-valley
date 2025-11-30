namespace Engine;

public class SpriteAnimation
{
    public SpriteSheet SpriteSheet { get; }
    public int[] Frames { get; }
    public float FrameDuration { get; }
    public bool Flip { get; }
    public bool Loop { get; }

    public int CurrentFrameIndex { get; set; } = 0;
    
    private float _frameTimer = 0f;
    
    public SpriteAnimation(SpriteSheet spriteSheet, int[] frames, float frameDuration = 0.1f, bool flip = false, bool loop = true)
    {
        SpriteSheet = spriteSheet;
        Frames = frames;
        FrameDuration = frameDuration;
        Flip = flip;
        Loop = loop;
    }

    public void Tick()
    {
        _frameTimer += Time.DeltaTime;
        if (_frameTimer >= FrameDuration)
        {
            CurrentFrameIndex = Loop ? (CurrentFrameIndex + 1) % Frames.Length : Math.Min(CurrentFrameIndex + 1, Frames.Length - 1);
            _frameTimer -= FrameDuration;
        }
    }

    public void Reset()
    {
        CurrentFrameIndex = 0;
        _frameTimer = 0f;
    }
}