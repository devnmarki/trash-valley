using Microsoft.Xna.Framework.Input;

namespace Engine;

public class InputAction
{
    public string Name { get; }
    public List<Keys> Keys { get; } = [];

    public InputAction(string name, IEnumerable<Keys>? keys = null)
    {
        Name = name;
        if (keys != null)
            Keys.AddRange(keys);
    }
}