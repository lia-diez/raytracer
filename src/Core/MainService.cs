using DependencyInjection.DefaultServices;
using raytracer;

namespace Core;

public class MainService
{
    private IConfiguration _config;
    private SceneService _sceneService;
    private IImageExporter _imageExporter;

    public MainService(IConfiguration config, SceneService sceneService, IImageExporter imageExporter)
    {
        _config = config;
        _sceneService = sceneService;
        _imageExporter = imageExporter;
    }

    public void Execute()
    {
        var scene = _sceneService.GetScene();
        var bitmap = scene.Camera.Render();
        _imageExporter.Destination = File.OpenWrite(_config["image"] ?? "out.png");
        _imageExporter.Bitmap = bitmap;
        _imageExporter.Export();
    }
}