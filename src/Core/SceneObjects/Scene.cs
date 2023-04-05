using Common.Light;
using Common.Primitives;

namespace Core.SceneObjects;

public class Scene
{
    public List<ITraceable> Traceables;
    public List<ILight> Lights;

    public Scene(List<ITraceable> traceables, List<ILight> lights)
    {
        Traceables = traceables;
        Lights = lights;
    }

    public Scene()
    {
        Traceables = new List<ITraceable>();
        Lights = new List<ILight>();
    }
}