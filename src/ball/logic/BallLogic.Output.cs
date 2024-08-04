using Godot;

namespace test.ball.logic;

public partial class BallLogic
{
    public static class Output
    {
        public readonly record struct Enabled;
        public readonly record struct Disabled;
        public readonly record struct Colliding;
        public readonly record struct MovementSpeedChanged(float MovementSpeed);
        public readonly record struct PositionChanged(Vector2 Position);
        public readonly record struct VelocityChanged(Vector2 Velocity);
    }
}