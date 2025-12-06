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
        
        SyncWithPlayer();
    }
    
    private void SyncWithPlayer()
    {
        EntityQuery()
            .QueryAll<ToolComponent, Transform>()
            .ForEach((Entity entity, ref ToolComponent tool, ref Transform transform) =>
            {
                ref PlayerComponent playerComponent = ref _playerEntity.GetComponent<PlayerComponent>();
                ref SpriteRenderer sr = ref entity.GetComponent<SpriteRenderer>();

                if (playerComponent.State != PlayerState.Action)
                {
                    playerComponent.CurrentTool = null;
                    entity.Destroy();
                    return;
                }

                ToolKeyFrame toolKeyFrame = ToolAnimationDatabase.Frames[tool.Model.Type][playerComponent.Direction];
                
                entity.Transform.Position = _playerEntity.Transform.Position + toolKeyFrame.Offset * Constants.ScaleFactor;
                sr.Sprite = tool.Model.SpriteSheet.GetSprite(toolKeyFrame.SpriteIndex);
                sr.Layer = toolKeyFrame.RenderingLayer;
            });
    }
}