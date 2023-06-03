using Common.Structures;
using Common.Structures.Numerics;

namespace Common.Primitives;

public record TraceResult(ITraceable Traceable, Vector3 Normal, Point IntersectionPoint);