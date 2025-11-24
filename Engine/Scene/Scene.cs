using Arch.Core;

namespace Engine;

public abstract class Scene
{
    public World EntityWorld { get; set; } = World.Create();

    public List<System> Systems { get; set; } = [];

    private PhysicsSystem _physicsSystem;

    public virtual void OnEnter()
    {
        AddSystem(new RenderSystem());    
        AddSystem(_physicsSystem = new PhysicsSystem());
    }
    
    public virtual void OnUpdate() { }
    public virtual void OnRender() { }

    public virtual void OnExit()
    {
        _physicsSystem?.Cleanup();
        
        Systems.Clear();
        
        EntityWorld.Dispose();
        EntityWorld = World.Create();
    }

    public Entity AddEntity<T>(params object[] args) where T : Entity
    {
        var entityId = EntityWorld.Create();
        Entity newEntity = (Entity)Activator.CreateInstance(typeof(T), [EntityWorld, entityId, ..args])!;
        return newEntity;
    }

    public void DestroyEntity(Entity entity)
    {
        entity.Destroy();
    }

    public void AddSystem(System system)
    {
        Systems.Add(system);
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