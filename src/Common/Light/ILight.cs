using Common.Structures;

namespace Common.Light;

public interface ILight
{
    public float ComputeColor(Vector normal);
}