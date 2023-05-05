using Common.Structures;
using Common.Structures.Numerics;

namespace Common.Light;

public class DirectionLight : ILight
{
    public Vector3 Direction;

    public DirectionLight(Vector3 direction)
    {
        Direction = direction.Normalize();
    }

    public float ComputeColor(Vector3 normal)
    {
        return Vector3.DotProduct(Direction, normal);
    }
}