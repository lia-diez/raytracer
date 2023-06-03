using System.Diagnostics;
using Common.Extensions;
using Common.Light;
using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;
using Core;
using Core.SceneObjects;
using MeshManipulation;
using raytracer;

var scene = new Scene();
scene.Lights.Add(new DirectionLight(new Vector3(1, 0, 0)));

var cow = ObjReader.ReadObj(@"C:\Users\proku\Downloads\cow.obj");
cow = Transformer.Transform(cow, new Matrix(4)
    .Translate(0, 0, 0.4f)
    .Rotate((float)Math.PI, 0, 0)
    .Scale(1f, 2f, 1f));
scene.Traceables.Add(cow);

var transformation = new Matrix(4)
    // .Scale(0.95f, 1.1f, 1.05f)
    .Translate(0, 0, -0.4f)
    .Rotate(0, -MathExtensions.DegreeToRad(100), 0)
    .Translate(0, 0, -0.9f);

var camera = new Camera(new CameraSettings()
    {
        Fov = 60,
        Resolution = new Vector2Int(120, 80),
        Transformation = transformation
    },
    scene);

var bitmap = camera.Render();

// var exporter = new AsciiImageExporter(Console.OpenStandardOutput(), bitmap);

var stream = File.Open("pic2.bmp", FileMode.OpenOrCreate);
var exporter = new BmpImageExporter(stream, bitmap);

exporter.Export();
stream.Close();
// Console.ReadKey();