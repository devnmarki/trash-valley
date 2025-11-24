namespace Engine;

public abstract class System
{
    public abstract void Update();
    public abstract void Render();

    protected EntityQuery EntityQuery()
    {
        return new EntityQuery(SceneManager.ActiveScene?.EntityWorld!);
    }
}