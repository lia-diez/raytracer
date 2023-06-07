using System.Diagnostics;
using Common.Structures;
using Common.Structures.Traceable;
using OptimisationTree;
using TimeTests;

var trianglesNum = 30000;
var boxesNum = trianglesNum;
        
(int min, int max) triangleSize = (1, 5);
var boxSize = triangleSize;
        
var raysNum = trianglesNum;
        
var sceneSize = (100, 100, 100);
        
var triangles = new List<Triangle>();
var boxes = new List<AxisBox>();
var rays = new List<Ray>();
        
for (var i = 0; i < trianglesNum; i++)
{
    triangles.Add(Generator.CreateTriangle(sceneSize, triangleSize));
    boxes.Add(Generator.CreateBox(sceneSize, boxSize));
    rays.Add(Generator.CreateRay(sceneSize));
}
        
var stopwatch1 = new Stopwatch();
stopwatch1.Start();
foreach (var triangle in triangles)
{
    foreach (var ray in rays)
    {
        triangle.Intersects(ray);
    }
}
stopwatch1.Stop();
Console.WriteLine("triangle");
Console.WriteLine(stopwatch1.Elapsed);
Console.WriteLine(stopwatch1.Elapsed / (trianglesNum * raysNum));
        
var stopwatch2 = new Stopwatch();
stopwatch2.Start();
foreach (var box in boxes)
{
    foreach (var ray in rays)
    {
        box.Intersects(ray);
    }
}
stopwatch2.Stop();
Console.WriteLine("box");
Console.WriteLine(stopwatch2.Elapsed);
Console.WriteLine(stopwatch2.Elapsed / (boxesNum * raysNum));