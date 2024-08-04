using Godot;

namespace test.common.utils.instantiator;

public interface IInstantiator
{
    /// <summary>
    /// The scene tree of the game.
    /// This will be used to instantiate nodes.
    /// </summary>
    public SceneTree SceneTree { get; }
    
    
    /// <summary>
    /// Loads and Instantiates a node from a path.
    /// </summary>
    /// <param name="path">The path to the scene file</param>
    /// <typeparam name="T">The type of the node to instantiate</typeparam>
    /// <returns>The instantiated node</returns>
    T Instantiate<T>(string path) where T : Node;
    
    /// <summary>
    /// Instantiates a node from a packed scene.
    /// </summary>
    /// <param name="scene">The packed scene to instantiate</param>
    /// <typeparam name="T">The type of the node to instantiate</typeparam>
    /// <returns>The instantiated node</returns>
    T Instantiate<T>(PackedScene scene) where T : Node;
}