using Godot;

namespace test.ball.logic;

public partial class BallLogic
{
    public static class Input
    {
        public readonly record struct Enable;
        public readonly record struct Disable;
        public readonly record struct PhysicsTick(double Delta);
        public readonly record struct Collision(Node2D Body);
        public readonly record struct CollisionDone;
    }
}