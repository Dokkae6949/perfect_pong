using Chickensoft.GodotNodeInterfaces;
using Godot;
using test.ball.logic;

namespace test.ball;

public interface IBall : IArea2D
{
    IBallLogic BallLogic { get; }
    
    Vector2 Velocity { get; set; }
}