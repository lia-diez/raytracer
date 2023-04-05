using Common.Structures;
using Core.SceneObjects;
using raytracer;

var camera = new Camera(new CameraSettings()
{
    Fov = 30,
    Resolution =  new Vector2Int(50,50)
});
var bitmap = camera.Render();

var exporter = new AsciiImageExporter(Console.OpenStandardOutput(), bitmap);
exporter.Export();
Console.ReadKey();

