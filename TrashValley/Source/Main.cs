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
        AssetManager.LoadTextures("Data/Assets/textures.json");
        AssetManager.LoadSpriteSheets("Data/Assets/spritesheets.json");
        
        SceneManager.AddScene("default_scene", new DefaultScene());
        SceneManager.AddScene("farm_scene", new FarmScene());
        SceneManager.SwitchScene("default_scene");

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
}