using Common.Structures;

namespace Common.Primitives;

public interface ITraceable
{
    public Point? FindIntersection(Ray ray);
    public Vector GetNormal(Point point);
}