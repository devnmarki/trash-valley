using Arch.Core;

namespace Engine;

public class Entity
{
    private readonly World _world;
    private readonly Arch.Core.Entity _id;

    public Transform Transform { get; set; }

    public Entity(World world, Arch.Core.Entity id)
    {
        _world = world;
        _id = id;
        
        Transform = AddComponent<Transform>();
    }

    public T AddComponent<T>(T component)
    {
        _world.Add(_id, component);
        return component;
    }
    
    public T AddComponent<T>(params object[] args)
    {
        T component;

        if (args.Length > 0)
            component = (T)Activator.CreateInstance(typeof(T), args)!;
        else
            component = Activator.CreateInstance<T>();
        
        _world.Add(_id, component);

        return component;
    }

    public ref T GetComponent<T>()
    {
        return ref _world.Get<T>(_id);
    }

    public bool HasComponent<T>()
    {
        return _world.Has<T>(_id);
    }

    public void RemoveComponent<T>()
    {
        _world.Remove<T>(_id);
    }

    public void Destroy()
    {
        _world.Destroy(_id);
    }
}