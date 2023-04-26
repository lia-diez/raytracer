using Common.Light;
using Common.Structures;
using Common.Structures.Traceable;
using Core.SceneObjects;
using raytracer;

var scene = new Scene();
scene.Lights.Add(new DirectionLight(new Vector(1, 0, 0)));
// scene.Traceables.Add(new Sphere(new Point(0, -2, 5), 0.5f));
// scene.Traceables.Add(new Sphere(new Point(1, -1, 5), 1));

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
// scene.Traceables.Add(new Sphere(new Point(-2, -3, 5), 0.5f));

var camera = new Camera(new CameraSettings()
{
    Fov = 30,
    Resolution =  new Vector2Int(1024,1024)
}, scene);
var bitmap = camera.Render();
var stream = File.Open("pic.bmp", FileMode.OpenOrCreate);
var exporter = new BmpImageExporter(stream, bitmap);
exporter.Export();
stream.Close();


