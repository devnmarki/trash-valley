using Microsoft.Xna.Framework;
using VelcroPhysics.Dynamics;

namespace Engine;

public interface ICollider
{
    Body? Body { get; set; }
}

public struct BoxCollider : ICollider
{
    public Vector2 Size;
    public Vector2 Offset;
    
    public Body? Body { get; set; }
    
    public BoxCollider(Vector2 size, Vector2 offset = default, BodyType bodyType = BodyType.Static)
    {
        Size = size;
        Offset = offset;
        Body = null;
    }
}

public struct Rigidbody
{
    public float Mass;
    public float GravityScale;
    public Vector2? Velocity;
    
    public ICollider? Collider;
    
    public Rigidbody(float mass = 1f, float gravityScale = 1f)
    {
        Mass = mass;
        GravityScale = gravityScale;
        Velocity = null;
        Collider = null;
    }
}