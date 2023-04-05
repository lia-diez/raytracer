using Common.Structures;

namespace Common.Primitives;

public interface ICamera
{
    public Ray[,] Render();
}