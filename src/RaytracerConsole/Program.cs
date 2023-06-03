using Common.Extensions;
using Common.Light;
using Common.Structures.Numerics;
using Core.SceneObjects;
using MeshManipulation;
using raytracer;

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

        var mesh = ObjReader.ReadObj(objFile);
        var scene = new Scene();

        scene.Lights.Add(new DirectionLight(new Vector3(1, 1, 0)));
        scene.Traceables.Add(mesh);

        var transformation = new Matrix(4)
            .Scale(0.95f, 1.1f, 1.05f)
            .Translate(0, 0, -0.4f)
            .Rotate(0, -MathExtensions.DegreeToRad(100), 0)
            .Translate(0, 0, -0.9f);

        var camera = new Camera(new CameraSettings()
        {
            Resolution = new Vector2Int(100, 100),
            Fov = 60,
            Transformation = transformation
        }, scene);

        var bitmap = camera.Render();

        var stream = File.Open(imageFile, FileMode.OpenOrCreate);
        var exporter = new BmpImageExporter(stream, bitmap);

        exporter.Export();
        stream.Close();
    }
}