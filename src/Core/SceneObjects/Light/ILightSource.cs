using Common.Primitives;
using Common.Structures;
using Common.Structures.Numerics;

namespace Core.SceneObjects.Light;

public interface ILightSource
{
    public Color Color { get; set; }
    public float Intensity { get; set; }
    public Color GetColor(TraceResult traceResult);
    public Vector3 GetDirection(TraceResult traceResult);
}