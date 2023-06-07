using System.Diagnostics;
using Common.Extensions;
using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;
using Core.SceneObjects;
using Core.SceneObjects.Light;
using DependencyInjection;
using MeshManipulation;
using raytracer;

namespace RaytracerConsole;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IImageExporter, BmpImageExporter>();
        services.AddSingleton<ObjReader>();
        
        var container = services.Build();

        string objFile = "", imageFile = "";
        foreach (string arg in args)
        {
            if (arg.StartsWith("--source="))
            {
                objFile = arg["--source=".Length..];
            }
            else if (arg.StartsWith("--output="))
            {
                imageFile = arg["--output=".Length..];
            }
        }

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        var scene = new Scene();
        
        var mesh = new TreeMesh(ObjReader.ReadObj(objFile));
        scene.Traceables.Add(mesh);
        
        scene.Lights.Add(new DirectionalLight(new Color(255, 0, 255), 0.8f, new Vector3(-1, 0, 1)));
        scene.Lights.Add(new AmbientLight(new Color(255, 255, 255), 0.05f));
        scene.Lights.Add(new SpotLight(new Color(0, 255, 255), 0.8f, new Point(1, 1, -1)));
        
        scene.Traceables.Add(new Sphere(new Point(0, 0, -1f), 0.5f));
        scene.Traceables.Add(new Sphere(new Point(-1.1f, 1, 1f), 0.5f));
        scene.Traceables.Add(new Sphere(new Point(0f, 1, 2f), 0.5f));
        scene.Traceables.Add(new Triangle(new Point(0, 0, 0), new Point(1, 0, 0), new Point(0, 0, 1)));

        var transformation = new Matrix(4)
            .Translate(0.4f, 1.4f, -0.29f)
            .Rotate(0, -(float)Math.PI / 2, 0)
            .Rotate(-MathExtensions.DegreeToRad(90), 0, 0)
            .Translate(0, 0, -2f);

        var camera = new Camera(new CameraSettings()
            {
                Fov = 80,
                Resolution = new Vector2Int(512, 512),
                Transformation = transformation
            },
            scene);
        
        var bitmap = camera.Render();

        var stream = File.Open(imageFile, FileMode.OpenOrCreate);
        var exporter = new BmpImageExporter(stream, bitmap);

        exporter.Export();
        stream.Close();

        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
    }
}