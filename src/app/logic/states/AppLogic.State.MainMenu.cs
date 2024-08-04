using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;
using test.ball.logic;

namespace test.app.logic;

public partial class AppLogic
{
    public partial record State
    {
        [Meta, Id("app_logic_state_main_menu")]
        public partial record MainMenu : State, IGet<Input.LoadGame>
        {
            public MainMenu()
            {
                this.OnEnter(() => Output(new Output.ShowSplashScreen()));
            }

            public Transition On(in Input.LoadGame input) => To<InGame>();
        }
    }
}