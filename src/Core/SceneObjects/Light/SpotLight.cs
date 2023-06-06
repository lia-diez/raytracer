using Common.Primitives;
using Common.Structures;
using Common.Structures.Numerics;

namespace Core.SceneObjects.Light;

public class SpotLight : ILightSource
{
    public SpotLight(Color color, float intensity, Point position)
    {
        Color = color;
        Intensity = intensity;
        Position = position;
    }

    public Color Color { get; set; }
    public float Intensity { get; set; }
    public Point Position { get; set; }

    public Color GetColor(TraceResult traceResult)
    {
        var dir = Position - traceResult.IntersectionPoint;
        var dist = dir.Magnitude;
        return Color * Intensity * Vector3.DotProduct(dir.Normalize(), traceResult.Normal) * (1f / (dist * dist));
    }

    public Vector3 GetDirection(TraceResult traceResult)
    {
        return Position - traceResult.IntersectionPoint;
        
    }
} 