using Engine;

namespace TrashValley;

public class MovementSystem : Engine.System
{
    public override void Update()
    {
        EntityQuery()
            .QueryAll<Rigidbody, MovementComponent>()
            .ForEach((Entity entity, ref Rigidbody rb, ref MovementComponent movement) =>
            {
                if (movement.Velocity.Length() > 0)
                    movement.Velocity.Normalize();
                
                rb.Velocity = movement.Velocity * movement.MoveSpeed * Time.DeltaTime;
            });
    }

    public override void Render()
    {
        
    }
}