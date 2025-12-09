using System;
using Engine;

namespace TrashValley;

public class ToolSystem : Engine.System
{
    private Entity _playerEntity;

    public override void Update()
    {
        base.Update();

        if (_playerEntity == null)
        {
            QueryPlayerEntity();
            return;
        }
        
        SyncWithPlayer();
    }

    private void QueryPlayerEntity()
    {
        _playerEntity = null;
            
        EntityQuery()
            .QueryAll<PlayerComponent>()
            .ForEach(entity =>
            {
                _playerEntity = entity;
            });
    }
    
    private void SyncWithPlayer()
    {
        EntityQuery()
            .QueryAll<ToolComponent, Transform>()
            .ForEach((Entity entity, ref ToolComponent tool, ref Transform transform) =>
            {
                if (_playerEntity == null)
                    return;
                
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

                ref SpriteRenderer playerSpriteRenderer = ref _playerEntity.GetComponent<SpriteRenderer>();
                sr.Layer = playerSpriteRenderer.Layer;
                sr.YSort = playerSpriteRenderer.YSort;
                sr.Layer += toolKeyFrame.RenderingLayer * 0.001f;
            });
    }
}