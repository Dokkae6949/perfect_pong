using Chickensoft.Introspection;
using Godot;

namespace test.ball.logic;

public partial class BallLogic
{
    [Meta, Id("player_logic_data")]
    public partial record Data
    {
        public Node2D? LastCollisionBody { get; set; }
    }
}