using Godot;
using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.Introspection;

namespace test.main_menu;

[Meta(typeof(IAutoNode))]
public partial class MainMenu : Control, IMainMenu
{
    #region Nodes
    [Node] public IButton StartGameButton { get; set; } = default!;
    #endregion

    #region Signals
    [Signal] public delegate void NewGameEventHandler();
    #endregion


    public void OnReady()
    {
        StartGameButton.Pressed += OnStartGameButtonPressed;
    }

    public void OnExitTree()
    {
        StartGameButton.Pressed -= OnStartGameButtonPressed;
    }


    private void OnStartGameButtonPressed() => EmitSignal(SignalName.NewGame);
}
