using Common.Structures;

namespace Core.SceneObjects;

public interface ICamera
{
    public Ray[,] Render();
}