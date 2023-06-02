using Common.Light;
using Common.Primitives;

namespace Core.SceneObjects;

public class Scene
{
    public ICamera Camera;
    public List<ITraceable> Traceables;
    public List<ILight> Lights;

    public Scene(ICamera camera, List<ITraceable> traceables, List<ILight> lights)
    {
        Camera = camera;
        Traceables = traceables;
        Lights = lights;
    }

    public Scene()
    {
        Traceables = new List<ITraceable>();
        Lights = new List<ILight>();
    }
}