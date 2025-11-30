using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine;

public class Input
{
    private static KeyboardState _previousKeyboardState;
    private static KeyboardState _currentKeyboardState;
    private static MouseState _previousMouseState;
    private static MouseState _currentMouseState;

    private static readonly Dictionary<string, InputAction> _actions = new();
    
    public static void UpdateState()
    {
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();

        _previousMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();
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

    public static bool IsMouseDown(MouseButtons button) =>
        button switch
        {
            MouseButtons.Left => _currentMouseState.LeftButton == ButtonState.Pressed,
            MouseButtons.Right => _currentMouseState.RightButton == ButtonState.Pressed,
            MouseButtons.Middle => _currentMouseState.MiddleButton == ButtonState.Pressed,
            _ => false
        };
    
    public static bool IsMousePressed(MouseButtons button) =>
        button switch
        {
            MouseButtons.Left => _currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released,
            MouseButtons.Right => _currentMouseState.RightButton == ButtonState.Pressed && _previousMouseState.RightButton == ButtonState.Released,
            MouseButtons.Middle => _currentMouseState.MiddleButton == ButtonState.Pressed && _previousMouseState.MiddleButton == ButtonState.Released,
            _ => false
        };
    
    public static bool IsMouseUp(MouseButtons button) => 
        button switch
        {
            MouseButtons.Left => _currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed,
            MouseButtons.Right => _currentMouseState.RightButton == ButtonState.Released && _previousMouseState.RightButton == ButtonState.Pressed,
            MouseButtons.Middle => _currentMouseState.MiddleButton == ButtonState.Released && _previousMouseState.MiddleButton == ButtonState.Pressed,
            _ => false
        };

    public static bool IsActionDown(string actionName)
    {
        _actions.TryGetValue(actionName, out var action);

        if (action == null)
            return false;
        
        return action.Keys.Any(IsKeyDown) || action.Buttons.Any(IsMouseDown);
    }

    public static bool IsActionPressed(string actionName)
    {
        _actions.TryGetValue(actionName, out var action);

        if (action == null)
            return false;

        return action.Keys.Any(IsKeyPressed) || action.Buttons.Any(IsMousePressed);
    }

    public static bool IsActionUp(string actionName)
    {
        _actions.TryGetValue(actionName, out var action);

        if (action == null)
            return false;

        return action.Keys.Any(IsKeyUp) || action.Buttons.Any(IsMouseUp);
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