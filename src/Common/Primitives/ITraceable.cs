using Common.Structures;
using Common.Structures.Numerics;

namespace Common.Primitives;

public interface ITraceable
{
    public TraceResult? Trace(Ray ray);
    public (bool, ITraceable?) Intersects(Ray ray);
}