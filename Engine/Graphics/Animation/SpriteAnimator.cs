namespace Engine;

public struct SpriteAnimator
{
    public readonly Dictionary<string, SpriteAnimation> Animations;
    public SpriteAnimation? CurrentAnimation;

    public SpriteAnimator()
    {
        Animations = new Dictionary<string, SpriteAnimation>();
        CurrentAnimation = null;
    }

    public void AddAnimation(string animationId, SpriteAnimation animation)
    {
        bool added = Animations.TryAdd(animationId, animation);

        if (!added)
            Console.WriteLine($"Animation {animationId} already exists!");
    }

    public void PlayAnimation(string animationId)
    {
        bool exists = Animations.TryGetValue(animationId, out var animation);

        if (!exists)
        {
            Console.WriteLine($"Animation {animationId} does not exist!");
            return;
        }

        if (animation != null && CurrentAnimation != animation)
        {
            CurrentAnimation = animation;
        }
    }
}