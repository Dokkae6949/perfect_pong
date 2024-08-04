using Godot;

namespace test.common.utils.instantiator;

public class Instantiator : IInstantiator
{
    public SceneTree SceneTree { get; }
    
    
    public Instantiator(SceneTree sceneTree)
    {
        SceneTree = sceneTree;
    }
    
    
    public T Instantiate<T>(string path) where T : Node
    {
        return GD.Load<PackedScene>(path).Instantiate<T>();
    }

    
    public T Instantiate<T>(PackedScene scene) where T : Node
    {
        return scene.Instantiate<T>();
    }
}