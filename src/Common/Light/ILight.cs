using Common.Structures;
using Common.Structures.Numerics;

namespace Common.Light;

public interface ILight
{
    public Vector3 Direction { get; set; }
    public float ComputeColor(Vector3 normal);
}