using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;
using test.game.data;

namespace test.game.logic;

public partial class GameLogic
{
    public partial record State
    {
        [Meta, Id("game_logic_state_starting")]
        public partial record Starting : State, IGet<Input.GameStarted>
        {
            public Starting()
            {
                this.OnEnter(() =>
                {
                    Output(new Output.StartGame());
                    Input(new Input.GameStarted());
                });
            }
            
            public Transition On(in Input.GameStarted input)
            {
                Get<IGameRepo>().OnGameStarted();

                return To<Running>();
            }
        }
    }
}