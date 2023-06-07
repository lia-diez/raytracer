using Common.Extensions;
using Common.Primitives;
using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;
using Core.SceneObjects.Light;

namespace Core.SceneObjects;

public static class StaticScenes
{
    public static List<SceneSettings> SceneFactory()
    {
        var list = new List<SceneSettings>();

        var sceneSettings1 = new SceneSettings
        {
            Scene = new Scene
            {
                Name = "cow-profile",
                Traceables = new List<ITraceable>
                {
                    new Triangle(new Point(0, 0, 0.5f), new Point(0.2f, 0, 0.5f), new Point(0, 0, 0.8f)),
                    new Triangle(new Point(-0.5f, 0, 0.5f), new Point(-0.3f, 0, 0.5f), new Point(-0.5f, 0, 0.8f)),
                    new Triangle(new Point(-0.5f, 0.2f, -0.4f), new Point(-0.3f, 0.2f, -0.5f), new Point(-0.5f, 0.2f, -0.8f)),
                    new Sphere(new Point(0.5f,0 ,0), 0.1f)
                },
                Lights = new List<ILightSource>
                {
                    new DirectionalLight(new Color(255, 0, 255), 0.8f, new Vector3(-1, 0, 1)),
                    new AmbientLight(new Color(255, 255, 255), 0.05f),
                    new SpotLight(new Color(0, 255, 255), 0.8f, new Point(1, 1, -1)),
                }
            },
            Paths = new List<string>
            {
                "cow.obj"
            }
        };

        sceneSettings1.Scene.Camera = new Camera(new CameraSettings
            {
                Fov = 80,
                Resolution = new Vector2Int(512, 512),
                // Resolution = new Vector2Int(100, 100),
                Transformation = new Matrix(4)
                    .Translate(0.4f, 1.4f, -0.29f)
                    .Rotate(0, -(float)Math.PI / 2, 0)
                    .Rotate(-MathExtensions.DegreeToRad(90), 0, 0)
                    .Translate(0, 0, -2f)
            },
            sceneSettings1.Scene);

        list.Add(sceneSettings1);

        
        var sceneSettings2 = new SceneSettings
        {
            Scene = new Scene
            {
                Name = "cow-alf",
                Traceables = new List<ITraceable>
                {
                    new Triangle(new Point(0, 0, 1), new Point(1, 0, 1), new Point(0, 0, 2)),
                    new Triangle(new Point(-1, 1, 1), new Point(-1, 0, 1), new Point(1, 0, 2)),
                    new Triangle(new Point(-1, 1, -1), new Point(-1, 0, -1), new Point(1, 0, -2)),
                },
                Lights = new List<ILightSource>
                {
                    new DirectionalLight(new Color(255, 0, 255), 0.8f, new Vector3(1, 1, 0)),
                    new AmbientLight(new Color(255, 255, 255), 0.05f),
                }
            },
            Paths = new List<string>
            {
                "cow.obj"
            },
            Transforms = new List<Matrix>
            {
                new Matrix(4)
                    .Scale(1f, 2f, 1f)
            }
        };
        
        sceneSettings2.Scene.Camera = new Camera(new CameraSettings
            {
                Fov = 60,
                Resolution = new Vector2Int(1024, 512),
                Transformation = new Matrix(4)
                    // .Scale(0.95f, 1.1f, 1.05f)
                    .Translate(0, 0, -0.4f)
                    .Rotate(0, -MathExtensions.DegreeToRad(100), 0)
                    .Translate(0, 0, -0.9f)
            },
            sceneSettings2.Scene);
        
        list.Add(sceneSettings2);

        return list;
    }
}