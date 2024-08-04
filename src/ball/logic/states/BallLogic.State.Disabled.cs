using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;
using test.game.data;

namespace test.ball.logic;

public partial class BallLogic
{
    public abstract partial record State
    {
        [Meta, Id("player_logic_state_disabled")]
        public partial record Disabled : State, IGet<Input.Enable>
        {
            public Disabled()
            {
                this.OnEnter(() => Output(new Output.Disabled()));
                
                OnAttach(() => Get<IGameRepo>().GameStarted += OnGameStarted);
                OnDetach(() => Get<IGameRepo>().GameStarted -= OnGameStarted);
            }

            public Transition On(in Input.Enable input) => To<Moving>();
            
            private void OnGameStarted() => Input(new Input.Enable());
        }
    }
}