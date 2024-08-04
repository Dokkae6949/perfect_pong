using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;
using test.app.data;

namespace test.app.logic;

public partial class AppLogic
{
    public partial record State
    {
        [Meta, Id("app_logic_state_in_game")]
        public partial record InGame : State, IGet<Input.EndGame>
        {
            public InGame()
            {
                this.OnEnter(() =>
                {
                    Get<IAppRepo>().OnGameStarted();
                    Output(new Output.ShowGame());
                });
                this.OnExit(() => Output(new Output.HideGame()));
                
                OnAttach(() => Get<IAppRepo>().GameEnded += OnGameEnded);
                OnDetach(() => Get<IAppRepo>().GameEnded -= OnGameEnded);
            }

            public Transition On(in Input.EndGame input)
            {
                return input.Action switch
                {
                    GameEndedAction.Quit => To<MainMenu>(),
                    GameEndedAction.GoToMainMenu => To<MainMenu>(),
                    _ => To<MainMenu>()
                };
            }

            private void OnGameEnded(GameEndedAction action) => Input(new Input.EndGame(action));
        }
    }
}