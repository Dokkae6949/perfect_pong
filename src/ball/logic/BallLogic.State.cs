using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;

namespace test.ball.logic;

public partial class BallLogic
{
    [Meta]
    public abstract partial record State : StateLogic<State>;
}