using Common.Primitives;
using Core.SceneObjects.Light;

namespace Core.SceneObjects;

public class Scene
{
    public string Name;
    public ICamera Camera;
    public List<ITraceable> Traceables;
    public List<ILightSource> Lights;

    public Scene()
    {
        Traceables = new List<ITraceable>();
        Lights = new List<ILightSource>();
    }
}