using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine;

public class Input
{
    private static KeyboardState _previousKeyboardState;
    private static KeyboardState _currentKeyboardState;

    private static readonly Dictionary<string, InputAction> _actions = new();
    
    public static void UpdateState()
    {
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();
    }

    public static void AddAction(InputAction action)
    {
        bool added = _actions.TryAdd(action.Name, action);

        if (!added)
            Console.WriteLine($"Could not add action: {action.Name}");
    }

    public static bool IsKeyDown(Keys key) => _currentKeyboardState.IsKeyDown(key);
    public static bool IsKeyPressed(Keys key) => _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
    public static bool IsKeyUp(Keys key) => _currentKeyboardState.IsKeyUp(key);

    public static bool IsActionDown(string actionName)
    {
        _actions.TryGetValue(actionName, out var action);

        if (action == null)
            return false;
        
        return action.Keys.Any(IsKeyDown);
    }

    public static bool IsActionPressed(string actionName)
    {
        _actions.TryGetValue(actionName, out var action);

        if (action == null)
            return false;

        return action.Keys.Any(IsKeyPressed);
    }

    public static bool IsActionUp(string actionName)
    {
        _actions.TryGetValue(actionName, out var action);

        if (action == null)
            return false;

        return action.Keys.Any(IsKeyUp);
    }

    public static float GetAxis(string negative, string positive)
    {
        float value = 0f;
        if (IsActionDown(negative)) value = -1f;
        if (IsActionDown(positive)) value = 1f;
        return value;
    }

    public static Vector2 GetVector(string negativeX, string positiveX, string negativeY, string positiveY)
    {
        Vector2 vector = Vector2.Zero;
        vector.X = GetAxis(negativeX, positiveX);
        vector.Y = GetAxis(negativeY, positiveY);
        return vector;
    } 
}