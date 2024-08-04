using System.Diagnostics;
using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;
using test.game.data;

namespace test.ball.logic;

public partial class BallLogic
{
    public abstract partial record State
    {
        [Meta, Id("player_logic_state_enabled_colliding")]
        public partial record Colliding : Enabled, IGet<Input.CollisionDone>
        {
            public Colliding()
            {
                this.OnEnter(() =>
                {
                    Output(new Output.Colliding());
                    
                    var ball = Get<IBall>();
                    var data = Get<Data>();
                    var gameRepo = Get<IGameRepo>();
                
                    Debug.Assert(data.LastCollisionBody != null, "LastCollisionBody should not be null");

                    // TODO: Implement collision response
                    // TODO: Test if multiple outputs can be sent at once
                
                    // 1. Detect what we hit
                
                    // 2. Reposition ball
                    Output(new Output.PositionChanged(ball.Position));
                
                    // 3. Calculate new velocity
                    Output(new Output.VelocityChanged(ball.Velocity));
                
                    // 4. Notify ball repo
                    gameRepo.OnBallCollided(BallCollidedAction.Wall);
                
                    Input(new Input.CollisionDone());
                });
            }

            public Transition On(in Input.CollisionDone input) => To<Moving>();
        }
    }
}