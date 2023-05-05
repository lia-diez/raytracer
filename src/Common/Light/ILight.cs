using Common.Structures;
using Common.Structures.Numerics;

namespace Common.Light;

public interface ILight
{
    public float ComputeColor(Vector3 normal);
}