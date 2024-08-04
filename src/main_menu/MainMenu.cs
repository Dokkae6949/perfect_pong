using Godot;
using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.Introspection;

namespace test.main_menu;

[Meta(typeof(IAutoNode))]
public partial class MainMenu : Control, IMainMenu
{
    #region Signals
    [Signal] public delegate void GameStartedEventHandler();
    #endregion

    #region Exports
    [Export] public Button StartGameButton { get; set; } = default!;
    #endregion


    public override void _Ready()
    {
        StartGameButton.Pressed += OnGameStartedButtonPressed;
    }


    public override void _ExitTree()
    {
        StartGameButton.Pressed -= OnGameStartedButtonPressed;
    }


    private void OnGameStartedButtonPressed()
    {
        EmitSignal(SignalName.GameStarted);
    }
}
