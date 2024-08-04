using test.app.data;

namespace test.app.logic;

public partial class AppLogic
{
    public static class Input
    {
        public readonly record struct SplashScreenFadedIn;
        public readonly record struct SplashScreenFadedOut;
        public readonly record struct LoadGame;
        public readonly record struct EndGame(GameEndedAction Action);
    }
}