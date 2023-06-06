using Common.Primitives;
using Common.Structures;
using Common.Structures.Numerics;

namespace Core.SceneObjects.Light;

public class DirectionalLight : ILightSource
{
    public DirectionalLight(Color color, float intensity, Vector3 direction)
    {
        Color = color;
        Intensity = intensity;
        Direction = direction.Normalize();
    }

    public Vector3 Direction { get; set; }
    public Color Color { get; set; }
    public float Intensity { get; set; }
    
    public Color GetColor(TraceResult traceResult)
    {
        return Vector3.DotProduct(traceResult.Normal, Direction) * Color * Intensity;
    }

    public Vector3 GetDirection(TraceResult traceResult)
    {
        return Direction;
    }
}