using Common.Primitives;
using Common.SceneObjects;
using Common.Structures;

var ray = new Ray(Point.Zero, new Vector(0, 0, 1));
var sphere = new Sphere(new Point(1, 2.5f, 5), 1);

var a = sphere.FindIntersection(ray);

var camera = new Camera(Point.Zero, new Vector(0, 0, 1), 30, (50, 50));
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