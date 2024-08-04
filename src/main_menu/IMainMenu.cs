using Chickensoft.GodotNodeInterfaces;

namespace test.main_menu;

public interface IMainMenu : IControl
{
    event MainMenu.GameStartedEventHandler GameStarted;
}