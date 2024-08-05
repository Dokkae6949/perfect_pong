using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;

namespace test.game.logic;

[Meta, Id("game_logic")]
[LogicBlock(typeof(State), Diagram = true)]
public partial class GameLogic : LogicBlock<GameLogic.State>, IGameLogic
{
    public override Transition GetInitialState() => To<State.Starting>();
}