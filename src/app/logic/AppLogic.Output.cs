namespace test.app.logic;

public partial class AppLogic
{
    public static class Output
    {
        public readonly record struct ShowSplashScreen;
        public readonly record struct HideSplashScreen;
        public readonly record struct ShowMainMenu;
        public readonly record struct HideMainMenu;
        public readonly record struct ShowGame;
        public readonly record struct HideGame;
    }
}