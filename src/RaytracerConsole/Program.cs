using System.Diagnostics;
using Common.Extensions;
using Common.Light;
using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;
using Core;
using Core.SceneObjects;
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
        Console.WriteLine("I don't have the motivation and energy to do all this anymore, I have tried everything and now I am exhausted.");
        
        var sceneSize = (10, 10, 10);
        var triangleSize = (1, 3);
        
        var num = 100000;
        var triangles = new List<Triangle>();
        for (int i = 0; i < num; i++)
        {
            triangles.Add(Generator.CreateTriangle(sceneSize, triangleSize));
        }
        
        var tree = new Tree();
        tree.Build(triangles);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        tree.Search(triangles[2]);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        // tree.Root.PrintPretty("", true);
    }
}