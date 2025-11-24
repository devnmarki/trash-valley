using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TrashValley;

public class Main : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

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
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        AssetManager.LoadTexture("player_spritesheet", "Sprites/player_sheet");
        AssetManager.LoadSpriteSheet("player_spritesheet", new SpriteSheetProps
        {
            Texture = AssetManager.GetTexture("player_spritesheet")!,
            Columns = 4,
            Rows = 9,
            Flip = false,
            SpriteSize = new Vector2(32)
        });
        
        SceneManager.AddScene("default_scene", new DefaultScene());
        SceneManager.AddScene("farm_scene", new FarmScene());
        SceneManager.SwitchScene("default_scene");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _engine.Update(gameTime);
        
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