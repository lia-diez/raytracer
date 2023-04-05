using Common.Structures;

namespace Common.Primitives;

public interface ITraceable
{
    public Point? FindIntersection(Ray ray);
}