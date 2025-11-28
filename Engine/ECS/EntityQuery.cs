using Arch.Core;

namespace Engine;

public delegate void EntityAction(Entity entity);
public delegate void EntityAction<T>(Entity entity, ref T c1);
public delegate void EntityAction<T1, T2>(Entity entity, ref T1 c1, ref T2 c2);
public delegate void EntityAction<T1, T2, T3>(Entity entity, ref T1 c1, ref T2 c2, ref T3 c3);
public delegate void EntityAction<T1, T2, T3, T4>(Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4);
public delegate void EntityAction<T1, T2, T3, T4, T5>(Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5);

public class EntityQuery
{
    private readonly World _world;
    private QueryDescription _query;

    public EntityQuery(World world)
    {
        _world = world;
        _query = new QueryDescription();
    }

    public EntityQuery QueryAll<T>()
    {
        _query = _query.WithAll<T>();
        return this;
    }
    
    public EntityQuery QueryAll<T1, T2>()
    {
        _query = _query.WithAll<T1, T2>();
        return this;
    }
    
    public EntityQuery QueryAll<T1, T2, T3>()
    {
        _query = _query.WithAll<T1, T2, T3>();
        return this;
    }
    
    public EntityQuery QueryAll<T1, T2, T3, T4>()
    {
        _query = _query.WithAll<T1, T2, T3, T4>();
        return this;
    }
    
    public EntityQuery QueryAll<T1, T2, T3, T4, T5>()
    {
        _query = _query.WithAll<T1, T2, T3, T4, T5>();
        return this;
    }
    
    public EntityQuery QueryAll<T1, T2, T3, T4, T5, T6>()
    {
        _query = _query.WithAll<T1, T2, T3, T4, T5, T6>();
        return this;
    }
    
    public EntityQuery QueryAll<T1, T2, T3, T4, T5, T6, T7>()
    {
        _query = _query.WithAll<T1, T2, T3, T4, T5, T6, T7>();
        return this;
    }
    
    public EntityQuery QueryAll<T1, T2, T3, T4, T5, T6, T7, T8>()
    {
        _query = _query.WithAll<T1, T2, T3, T4, T5, T6, T7, T8>();
        return this;
    }
    
    public EntityQuery QueryAll<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
    {
        _query = _query.WithAll<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
        return this;
    }
    
    public EntityQuery QueryAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
    {
        _query = _query.WithAll<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
        return this;
    }

    public EntityQuery Exclude<T>()
    {
        _query = _query.WithNone<T>();
        return this;
    }

    public void ForEach(EntityAction action)
    {
        _world.Query(in _query, archEntity =>
        {
            var entity = new Entity(_world, archEntity);
            action(entity);
        });
    }
    
    public void ForEach<T>(EntityAction<T> action)
    {
        _world.Query(in _query, (Arch.Core.Entity archEntity, ref T c1) =>
        {
            var entity = new Entity(_world, archEntity);
            action(entity, ref c1);
        });
    }
    
    public void ForEach<T1, T2>(EntityAction<T1, T2> action)
    {
        _world.Query(in _query, (Arch.Core.Entity archEntity, ref T1 c1, ref T2 c2) =>
        {
            var entity = new Entity(_world, archEntity);
            action(entity, ref c1, ref c2);
        });
    }
    
    public void ForEach<T1, T2, T3>(EntityAction<T1, T2, T3> action)
    {
        _world.Query(in _query, (Arch.Core.Entity archEntity, ref T1 c1, ref T2 c2, ref T3 c3) =>
        {
            var entity = new Entity(_world, archEntity);
            action(entity, ref c1, ref c2, ref c3);
        });
    }
    
    public void ForEach<T1, T2, T3, T4>(EntityAction<T1, T2, T3, T4> action)
    {
        _world.Query(in _query, (Arch.Core.Entity archEntity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4) =>
        {
            var entity = new Entity(_world, archEntity);
            action(entity, ref c1, ref c2, ref c3, ref c4);
        });
    }
    
    public void ForEach<T1, T2, T3, T4, T5>(EntityAction<T1, T2, T3, T4, T5> action)
    {
        _world.Query(in _query, (Arch.Core.Entity archEntity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5) =>
        {
            var entity = new Entity(_world, archEntity);
            action(entity, ref c1, ref c2, ref c3, ref c4, ref c5);
        });
    }
}