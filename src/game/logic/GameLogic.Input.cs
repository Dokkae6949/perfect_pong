using test.game.data;

namespace test.game.logic;

public partial class GameLogic
{
    public static class Input
    {
        public readonly record struct EndGame(GameEndedReason reason);
    }
}