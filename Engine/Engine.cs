using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine;

public class Engine
{
    public static Engine Instance { get; private set; } = null!;

    public Game Game { get; private set; }
    
    public ContentManager Content { get; private set; }
    public SpriteBatch SpriteBatch { get; private set; }
    
    private Engine(Game game)
    {
        Game = game;

        Content = game.Content;
        SpriteBatch = new SpriteBatch(game.GraphicsDevice);
    }

    public static Engine Create(Game game)
    {
        if (Instance != null)
            throw new Exception("Engine instance already created");

        Instance = new Engine(game);
        
        return Instance;
    }

    public void Update(GameTime gameTime)
    {
        Time.GameTime = gameTime;
        Time.DeltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
        
        Input.UpdateState();
        SceneManager.UpdateActiveScene();
    }

    public void Render()
    {
        SpriteBatch.Begin(sortMode: SpriteSortMode.Deferred, samplerState: SamplerState.PointWrap);
        SceneManager.RenderActiveScene();
        SpriteBatch.End();
    }
}