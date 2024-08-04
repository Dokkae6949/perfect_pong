using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;

namespace test.ball.logic;

[Meta, Id("ball_logic")]
[LogicBlock(typeof(State), Diagram = true)]
public partial class BallLogic : LogicBlock<BallLogic.State>, IBallLogic
{
    public override Transition GetInitialState() => To<State.Disabled>();
}