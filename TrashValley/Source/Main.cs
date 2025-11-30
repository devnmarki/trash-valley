using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TrashValley;

public class Main : Game
{
    private GraphicsDeviceManager _graphics;

    private Engine.Engine _engine;

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _engine = Engine.Engine.Create(this);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        InitializeInputActions();
        LoadAssets();
        InitializeScenes();

        Engine.Engine.Instance.DebugMode = true;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _engine.Update(gameTime);

        if (Input.IsKeyPressed(Keys.Tab))
            Engine.Engine.Instance.DebugMode = !Engine.Engine.Instance.DebugMode;
        
        if (Input.IsKeyPressed(Keys.Q))
            SceneManager.SwitchScene("default_scene");
        else if (Input.IsKeyPressed(Keys.E))
            SceneManager.SwitchScene("farm_scene");

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _engine.Render();
        
        base.Draw(gameTime);
    }

    private static void InitializeInputActions()
    {
        Input.AddAction(new InputAction("move_up", [Keys.W, Keys.Up]));
        Input.AddAction(new InputAction("move_down", [Keys.S, Keys.Down]));
        Input.AddAction(new InputAction("move_left", [Keys.A, Keys.Left]));
        Input.AddAction(new InputAction("move_right", [Keys.D, Keys.Right]));
    }

    private static void LoadAssets()
    {
        AssetManager.LoadTextures("Data/Assets/textures.json");
        AssetManager.LoadSpriteSheets("Data/Assets/spritesheets.json");
    }

    private static void InitializeScenes()
    {
        SceneManager.AddScene("default_scene", new DefaultScene());
        SceneManager.AddScene("farm_scene", new FarmScene());
        SceneManager.SwitchScene("default_scene");
    }
}