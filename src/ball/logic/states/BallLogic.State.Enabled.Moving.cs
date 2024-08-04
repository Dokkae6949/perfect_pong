using Chickensoft.Introspection;
using Godot;

namespace test.ball.logic;

public partial class BallLogic
{
    public abstract partial record State
    {
        [Meta, Id("player_logic_state_enabled_moving")]
        public partial record Moving : Enabled, IGet<Input.PhysicsTick>
        {
            public Transition On(in Input.PhysicsTick input)
            {
                var delta = (float) input.Delta;
                var ball = Get<IBall>();
                
                var newPosition = ball.Position + ball.Velocity * delta;

                Output(new Output.PositionChanged(newPosition));
                
                return ToSelf();
            }
        }
    }
}