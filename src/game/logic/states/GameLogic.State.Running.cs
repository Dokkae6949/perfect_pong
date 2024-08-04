using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;
using test.game.data;

namespace test.game.logic;

public partial class GameLogic
{
    public partial record State
    {
        [Meta, Id("game_logic_state_running")]
        public partial record Running : State, IGet<Input.EndGame>
        {
            public Running()
            {
                this.OnEnter(() =>
                {
                    Output(new Output.StartGame());
                    Get<IGameRepo>().OnGameStarted();
                });
                
                OnAttach(() => Get<IGameRepo>().GameEnded += OnGameEnded);
                OnDetach(() => Get<IGameRepo>().GameEnded -= OnGameEnded);
            }
            
            public Transition On(in Input.EndGame input)
            {
                Get<IGameRepo>().Pause();

                // TODO: Transition to GameOver state
                return ToSelf();
            }
            
            private void OnGameEnded(GameEndedReason reason) => Input(new Input.EndGame(reason));
        }
    }
}