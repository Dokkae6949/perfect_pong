using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;
using Godot;

namespace test.ball.logic;

public partial class BallLogic
{
    public abstract partial record State
    {
        [Meta, Id("player_logic_state_enabled")]
        public partial record Enabled : State, IGet<Input.Disable>, IGet<Input.Collision>
        {
            public Enabled()
            {
                this.OnEnter(() => Output(new Output.Enabled()));
            }

            public Transition On(in Input.Disable input) => To<Disabled>();

            public Transition On(in Input.Collision input)
            {
                Get<Data>().LastCollisionBody = input.Body;
                return To<Colliding>();
            }
        }
    }
}