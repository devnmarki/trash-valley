using Microsoft.Xna.Framework.Input;

namespace Engine;

public class Input
{
    private static KeyboardState _previousKeyboardState;
    private static KeyboardState _currentKeyboardState;
    
    public static void UpdateState()
    {
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();
    }

    public static bool IsKeyDown(Keys key) => _currentKeyboardState.IsKeyDown(key);
    public static bool IsKeyPressed(Keys key) => _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
    public static bool IsKeyUp(Keys key) => _currentKeyboardState.IsKeyUp(key);
}