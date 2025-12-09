using System.Transactions;
using Arch.Core;
using Engine;
using Entity = Engine.Entity;

namespace TrashValley;

public class Resource : Entity
{
    protected ref ResourceComponent ResourceComponentRef => ref GetComponent<ResourceComponent>();
    
    public Resource(World world, Arch.Core.Entity id, ResourceModel model) : base(world, id)
    {
        if (!HasComponent<ResourceComponent>())
            AddComponent(new ResourceComponent() { Model = model } );

        AddComponent(new SpriteRenderer()
        {
            Sprite = model.Sprite,
            Layer = Constants.Layers.Player,
            YSort = true
        });
    }
}