using Common.Structures;

namespace Common.Light;

public class DirectionLight : ILight
{
    public Vector Direction;

    public DirectionLight(Vector direction)
    {
        Direction = direction.Normalize();
    }

    public float ComputeColor(Vector normal)
    {
        return Vector.DotProduct(Direction, normal);
    }
}