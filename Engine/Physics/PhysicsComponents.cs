using Microsoft.Xna.Framework;
using VelcroPhysics.Dynamics;

namespace Engine;

public struct BoxCollider
{
    public Vector2 Size;
    public Vector2 Offset;

    internal Body? Body;
    
    public BoxCollider(Vector2 size, Vector2 offset = default, BodyType bodyType = BodyType.Static)
    {
        Size = size;
        Offset = offset;
        Body = null;
    }
}