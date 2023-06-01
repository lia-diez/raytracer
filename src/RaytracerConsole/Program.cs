using Common.Extensions;
using Common.Light;
using Common.Structures;
using Common.Structures.Traceable;
using Common.Structures.Numerics;
using Core.SceneObjects;
using raytracer;

var scene = new Scene();
scene.Lights.Add(new DirectionLight(new Vector3(1, 0, 0)));

var radius = 0.5f;
scene.Traceables.Add(new Sphere(new Point(0, 0, 10), radius));
for (int i = 1; i < 5; i++)
{
    scene.Traceables.Add(new Sphere(new Point(-i, i, 10), radius));
    scene.Traceables.Add(new Sphere(new Point(i, i, 10), radius));
    scene.Traceables.Add(new Sphere(new Point(i, -i, 10), radius));
    scene.Traceables.Add(new Sphere(new Point(-i, -i, 10), radius));
    scene.Traceables.Add(new Sphere(new Point(0, i, 10), radius));
    scene.Traceables.Add(new Sphere(new Point(0, -i, 10), radius));
    scene.Traceables.Add(new Sphere(new Point(i, 0, 10), radius));
    scene.Traceables.Add(new Sphere(new Point(-i, 0, 10), radius));
}

// var transformation = Matrix.Identity(4).Rotate(MathExtensions.DegreeToRad(10), 0, 0);
// var transformation = Matrix.Identity(4).Translate(0, -0.5f, 0);
Matrix transformation = null;

var camera = new Camera(new CameraSettings()
{
    Fov = 80,
    Resolution =  new Vector2Int(60,100),
    Origin = new Point(0, 0, 0),
    Direction = new Vector3(0, 0, 1),
    Transformation = transformation
}, 
    scene);

var bitmap = camera.Render();
var exporter = new AsciiImageExporter(Console.OpenStandardOutput(), bitmap);

// var stream = File.Open("pic2.bmp", FileMode.OpenOrCreate);
// var exporter = new BmpImageExporter(stream, bitmap);

exporter.Export();
// stream.Close();
// Console.ReadKey();