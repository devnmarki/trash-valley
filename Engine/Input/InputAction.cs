using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;

namespace Engine;

public class InputAction
{
    public string Name { get; }
    public List<Keys> Keys { get; } = [];
    public List<MouseButtons> Buttons { get; } = [];

    public InputAction(string name, IEnumerable<Keys>? keys = null, IEnumerable<MouseButtons>? buttons = null)
    {
        Name = name;
        if (keys != null)
            Keys.AddRange(keys);
        if (buttons != null)
            Buttons.AddRange(buttons);
    }
}