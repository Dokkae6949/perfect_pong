using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;

namespace test.app.logic;

[Meta, Id("app_logic")]
[LogicBlock(typeof(State), Diagram = true)]
public partial class AppLogic : LogicBlock<AppLogic.State>, IAppLogic
{
    public override Transition GetInitialState() => To<State.SplashScreen>();
}