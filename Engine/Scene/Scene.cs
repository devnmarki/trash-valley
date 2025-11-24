using Arch.Core;

namespace Engine;

public abstract class Scene
{
    public World EntityWorld { get; set; } = World.Create();

    public List<System> Systems { get; set; } = [];

    public virtual void OnEnter()
    {
        AddSystem(new RenderSystem());    
    }
    
    public virtual void OnUpdate() { }
    public virtual void OnRender() { }
    public virtual void OnExit() { }

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