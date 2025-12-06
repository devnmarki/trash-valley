using Arch.Core;
using MonoGame.Extended;

namespace Engine;

public abstract class Scene
{
    public World EntityWorld { get; set; } = World.Create();
    public List<System> Systems { get; set; } = [];
    public PhysicsSystem? PhysicsSystem { get; private set; }

    public OrthographicCamera? SceneCamera { get; set; }
    
    public virtual void OnEnter()
    {
        SceneCamera = new OrthographicCamera(Engine.Instance.ViewportAdapter);
        
        AddSystem(new RenderSystem());    
        AddSystem(PhysicsSystem = new PhysicsSystem());
    }
    
    public virtual void OnUpdate() { }
    public virtual void OnRender() { }

    public virtual void OnExit()
    {
        PhysicsSystem?.Cleanup();
        
        Systems.Clear();
        
        EntityWorld.Dispose();
        EntityWorld = World.Create();
    }

    public Entity AddEntity<T>(params object[] args) where T : Entity
    {
        var entityId = EntityWorld.Create();

        object[] ctorArgs = new object[args.Length + 2];
        ctorArgs[0] = EntityWorld;
        ctorArgs[1] = entityId;
        Array.Copy(args, 0, ctorArgs, 2, args.Length);

        return (Entity)Activator.CreateInstance(typeof(T), ctorArgs)!;
    }

    public void DestroyEntity(Entity entity)
    {
        entity.Destroy();
    }

    public void AddSystem(System system)
    {
        Systems.Add(system);
        system.Start();
    }

    public void RemoveSystem(System system)
    {
        Systems.Remove(system);
    }

    public EntityQuery EntityQuery()
    {
        return new EntityQuery(EntityWorld);
    }
}