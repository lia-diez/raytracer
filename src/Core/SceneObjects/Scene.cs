using Common.Primitives;
using Core.SceneObjects.Light;

namespace Core.SceneObjects;

public class Scene
{
    public ICamera Camera;
    public List<ITraceable> Traceables;
    public List<ILightSource> Lights;

    public Scene(ICamera camera, List<ITraceable> traceables, List<ILightSource> lights)
    {
        Camera = camera;
        Traceables = traceables;
        Lights = lights;
    }

    public Scene()
    {
        Traceables = new List<ITraceable>();
        Lights = new List<ILightSource>();
    }
}