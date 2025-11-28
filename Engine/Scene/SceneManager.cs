using Arch.Core;

namespace Engine;

public class SceneManager
{
    public static Dictionary<string, Scene> Scenes { get; } = [];
    public static Scene? ActiveScene { get; private set; } = null;

    public static void AddScene(string id, Scene scene)
    {
        if (!Scenes.TryAdd(id, scene))
        {
            Console.WriteLine($"Scene '{id}' already exists.");
        }
    }

    public static void SwitchScene(string id)
    {
        bool valid = Scenes.TryGetValue(id, out var newScene);
        if (!valid)
        {
            Console.WriteLine($"Unknown scene: {id}.");
            return;
        }

        if (ActiveScene != newScene)
        {
            ActiveScene?.OnExit();
            ActiveScene = newScene;
            ActiveScene?.OnEnter();
        }
    }

    public static void UpdateActiveScene()
    {
        if (ActiveScene == null) return;
        
        ActiveScene.OnUpdate();

        foreach (var system in ActiveScene.Systems)
        {
            system.Update();
        }
    }

    public static void RenderActiveScene()
    {
        if (ActiveScene == null) return;
        
        ActiveScene.OnRender();

        foreach (var system in ActiveScene.Systems)
        {
            system.Render();
        }
    }
}