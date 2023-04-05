using Common.Structures;
using Common.Structures.Traceable;
using Core.SceneObjects;

var sphere = new Sphere(new Point(0, 0, 5), 1);

var camera = new Camera(new CameraSettings()
{
    Fov = 30,
    Resolution =  new Rect(50,50)
});
var rays = camera.Render();

for (int i = 0; i < rays.GetLength(0); i++)
{
    for (int j = 0; j < rays.GetLength(1); j++)
    {
        Console.Write(sphere.FindIntersection(rays[i, j]) == null ? "  " : "00");
    }

    Console.WriteLine();
}

Console.ReadLine();