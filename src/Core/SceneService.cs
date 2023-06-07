using Core.SceneObjects;
using DependencyInjection.DefaultServices;
using MeshManipulation;

namespace Core;

public class SceneService
{
    private IConfiguration _config;
    private ObjReader _reader;

    public SceneService(IConfiguration config, ObjReader reader)
    {
        _config = config;
        _reader = reader;
    }

    public Scene GetScene()
    {
        if (_config["scene"] == null)
        {
            //читаєм з файлу
            var path = _config["path"];
        }
        else
        {
            var name = _config["scene"];
            
        }

        return new Scene();
    }
}