using System.Diagnostics;
using Common.Extensions;
using Common.Light;
using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;
using Core;
using Core.SceneObjects;
using Core.Transformation;
using MeshManipulation;
using OptimisationTree;
using OptimisationTree.Trees;
using raytracer;
using TimeTests;

namespace RaytracerConsole;

class Program
{
    static void Main(string[] args)
    {
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
        
        var mesh = new TreeMesh(ObjReader.ReadObj(objFile));
        var sphere = new Sphere(new Point(0, 0, -1.4f), 1f);
        
        var scene = new Scene();
        
        scene.Lights.Add(new DirectionLight(new Vector3(0, 0, 1)));
        scene.Traceables.Add(mesh);
        scene.Traceables.Add(sphere);

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