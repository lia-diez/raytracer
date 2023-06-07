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

    public void Execute(bool useTree)
    {
        var scene = _sceneService.GetScene(useTree);
        var bitmap = scene.Camera.Render();
        var stream = File.OpenWrite((useTree ? "" : "notree") + (_config["output"] ?? "out.bmp"));
        _imageExporter.Destination = stream;
        _imageExporter.Bitmap = bitmap;
        _imageExporter.Export();
        stream.Close();
    }
}