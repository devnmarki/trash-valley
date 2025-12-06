using System;
using Engine;

namespace TrashValley;

public class ToolSystem : Engine.System
{
    private Entity _playerEntity;
    
    public override void Start()
    {
        base.Start();
        
        EntityQuery()
            .QueryAll<PlayerComponent>()
            .ForEach(entity =>
            {
                _playerEntity = entity;
            });
    }

    public override void Update()
    {
        base.Update();
        
        EntityQuery()
            .QueryAll<ToolComponent, Transform>()
            .ForEach((Entity entity, ref ToolComponent tool, ref Transform transform) =>
            {
                ref PlayerComponent playerComponent = ref _playerEntity.GetComponent<PlayerComponent>();

                if (playerComponent.State != PlayerState.Action)
                    return;

                entity.Transform.Position = _playerEntity.Transform.Position;
            });
    }
}