using System.Diagnostics;
using Common.Extensions;
using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;
using Core;
using Core.SceneObjects;
using Core.SceneObjects.Light;
using DependencyInjection;
using DependencyInjection.DefaultServices;
using MeshManipulation;
using raytracer;

namespace RaytracerConsole;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddArgs();
        services.AddTransient<IImageExporter, BmpImageExporter>();
        services.AddSingleton<ObjReader>();
        services.AddSingleton<SceneService>();
        services.AddSingleton<MainService>();
        
        var container = services.Build();
        var main = container.GetService<MainService>();
        main.Execute(true);
        // main.Execute(false);
    }
}