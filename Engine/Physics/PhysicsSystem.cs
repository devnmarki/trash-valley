using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VelcroPhysics.Collision.Shapes;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;

namespace Engine;

public class PhysicsSystem : System
{
    private World? _physicsWorld;

    public PhysicsSystem()
    {
        _physicsWorld = new World(new Vector2(0f, 9.81f));
    }
    
    public override void Update()
    {
        GenerateBodies();
        
        _physicsWorld?.Step(Time.DeltaTime);
        
        SyncTransforms();
    }

    private void GenerateBodies()
    {
        EntityQuery()
            .QueryAll<Transform, BoxCollider>()
            .ForEach((Entity entity, ref Transform t, ref BoxCollider bc) =>
            {
                if (bc.Body == null)
                {
                    CreateBoxBody(entity, ref t, ref bc);
                }
            });
    }

    private void CreateBoxBody(Entity entity, ref Transform transform, ref BoxCollider collider)
    {
        float width = PhysicsUtils.ToMeters(collider.Size.X);
        float height = PhysicsUtils.ToMeters(collider.Size.Y);
        
        Body body = BodyFactory.CreateRectangle(
            world: _physicsWorld,
            width: width,
            height: height,
            density: 0f,
            position: PhysicsUtils.ToMeters(transform.Position + collider.Offset)
        );
        
        body.BodyType = entity.HasComponent<Rigidbody>() ? BodyType.Dynamic : BodyType.Static;
        body.Restitution = 0f;
        body.Friction = 0f;
        body.FixedRotation = true;
        
        collider.Body = body;
    }

    private void SyncTransforms()
    {
        EntityQuery()
            .QueryAll<Transform, BoxCollider, Rigidbody>()
            .ForEach((Entity entity, ref Transform t, ref BoxCollider c, ref Rigidbody rb) =>
            {
                rb.Collider ??= c;
                rb.Velocity = c.Body?.LinearVelocity ?? Vector2.Zero;

                if (c.Body != null)
                {
                    c.Body.Mass = rb.Mass;
                    c.Body.GravityScale = rb.GravityScale;
                }
            });
        
        EntityQuery()
            .QueryAll<Transform, BoxCollider>()
            .ForEach((Entity entity, ref Transform t, ref BoxCollider bc) =>
            {
                ref Rigidbody rb = ref entity.GetComponent<Rigidbody>();

                if (bc.Body == null) return;
                
                if (rb.GravityScale > 0f)
                {
                    t.Position = PhysicsUtils.ToPixels(bc.Body.Position) - bc.Offset;
                    t.Rotation = bc.Body.Rotation;
                }
                else
                {
                    bc.Body.Position = PhysicsUtils.ToMeters(t.Position + bc.Offset);
                    bc.Body.Rotation = t.Rotation;
                    bc.Body.LinearVelocity = Vector2.Zero;
                    bc.Body.AngularVelocity = 0f;
                }
            });
    }

    public override void Render()
    {
        EntityQuery()
            .QueryAll<Transform, BoxCollider>()
            .ForEach((Entity entity, ref Transform t, ref BoxCollider bc) =>
            {
                if (bc.Body == null) return;
                
                Color colliderColor = bc.Body.BodyType == BodyType.Dynamic ? Color.Red : Color.Green;
                
                Engine.Instance.SpriteBatch.Draw(
                    RenderHelper.Pixel,
                    PhysicsUtils.ToPixels(bc.Body.Position),
                    null,
                    colliderColor * 0.3f,
                    bc.Body.Rotation,
                    new Vector2(0.5f),
                    bc.Size,
                    SpriteEffects.None,
                    0.9f
                );
            });
    }

    public void Cleanup()
    {
        _physicsWorld?.Clear();
        _physicsWorld = null;
    }

    public void SetGravity(Vector2 gravity)
    {
        if (_physicsWorld != null) 
            _physicsWorld.Gravity = gravity;
    }
}