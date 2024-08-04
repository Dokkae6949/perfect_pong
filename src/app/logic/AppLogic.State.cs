using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;

namespace test.app.logic;

public partial class AppLogic
{
    [Meta]
    public abstract partial record State : StateLogic<State>;
}