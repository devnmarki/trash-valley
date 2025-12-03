namespace Engine;

public abstract class System
{
    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void Render() { }

    protected EntityQuery EntityQuery()
    {
        return new EntityQuery(SceneManager.ActiveScene?.EntityWorld!);
    }
}