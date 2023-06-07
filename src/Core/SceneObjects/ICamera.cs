using Common.Primitives;

namespace Core.SceneObjects;

public interface ICamera
{
    public Scene Scene { get; set; }
    public IBitmap Render();
}