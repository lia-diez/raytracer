using Common.Structures;
using Common.Structures.Numerics;

namespace Common.Primitives;

public interface ITraceable
{
    public Point? FindIntersection(Ray ray);
    public Vector3 GetNormal(Point point);
}