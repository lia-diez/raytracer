using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;
using Core.SceneObjects;
using Core.SceneObjects.Light;
using Core.Transformation;
using DependencyInjection.DefaultServices;
using MeshManipulation;

namespace Core;

public class SceneService
{
    private IConfiguration _config;
    private ObjReader _reader;

    public SceneService(IConfiguration config, ObjReader reader)
    {
        _config = config;
        _reader = reader;
    }

    public Scene GetScene(bool useTree)
    {
        var scene = new Scene();
        if (_config["scene"] == null)
        {
            var path = _config["source"];
            var triangles = _reader.ReadObj(path);
            scene.Camera = new Camera( new CameraSettings
            {
                Fov = 60,
                Resolution = new Vector2Int(100, 100)
            }, scene);
            if (useTree)
                scene.Traceables.Add(new TreeMesh(triangles));
            else
                scene.Traceables.Add(new Mesh(triangles));
            scene.Lights.Add(new DirectionalLight(new Color(255), 0.8f, new Vector3(1, 1, 1)));
            scene.Lights.Add(new SpotLight(new Color(255, 0, 0), 0.8f, new Point(0, 0, 0)));
        }
        else
        {
            var name = _config["scene"];
            var staticScenes = StaticScenes.SceneFactory();
            if (staticScenes.All(s => s.Scene.Name != name))
                throw new Exception($"Scene {name} does not exist");

            var settings = staticScenes.First(s => s.Scene.Name == name);
            scene.Traceables = settings.Scene.Traceables;

            for (var i = 0; i < settings.Paths.Count; i++)
            {
                var transform = settings.Transforms?[i] ?? new Matrix(4);
                var path = settings.Paths[i];
                
                if (useTree)
                    scene.Traceables.Add(Transformer.Transform(new TreeMesh(_reader.ReadObj(path)), transform));
                else
                    scene.Traceables.Add(Transformer.Transform(new Mesh(_reader.ReadObj(path)), transform));
                
            }

            scene.Camera = settings.Scene.Camera;
            scene.Camera.Scene = scene;
            scene.Lights = settings.Scene.Lights;
        }

        return scene;
    }
}