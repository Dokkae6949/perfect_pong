using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;

namespace test.game.logic;

public partial class GameLogic
{
    [Meta]
    public abstract partial record State : StateLogic<State>;
}