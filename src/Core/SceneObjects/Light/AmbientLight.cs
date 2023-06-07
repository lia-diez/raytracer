using Common.Primitives;
using Common.Structures;
using Common.Structures.Numerics;

namespace Core.SceneObjects.Light;

public class AmbientLight : ILightSource
{
    public AmbientLight(Color color, float intensity)
    {
        Color = color;
        Intensity = intensity;
    }
    
    public Color Color { get; set; }
    public float Intensity { get; set; }

    public Color GetColor(TraceResult traceResult)
    {
        return Intensity * Color;
    }

    public Vector3 GetDirection(TraceResult traceResult)
    {
        return Vector3.Zero;
    }
}