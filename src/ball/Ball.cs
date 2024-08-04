using Chickensoft.AutoInject;
using Chickensoft.Introspection;
using Godot;
using test.ball.logic;
using test.game.data;

namespace test.ball;

[Meta(typeof(IAutoNode))]
public partial class Ball : Area2D, IBall, IProvide<IBallLogic>
{
    public override void _Notification(int what) => this.Notify(what);

    #region Provisions
    IBallLogic IProvide<IBallLogic>.Value() => BallLogic;
    #endregion

    #region Dependencies
    [Dependency] public IGameRepo GameRepo => this.DependOn<IGameRepo>();
    #endregion
    
    #region Exports
    [Export] public Vector2 Velocity { get; set; }
    #endregion
    
    public IBallLogic BallLogic { get; set; } = default!;
    public BallLogic.IBinding BallBinding { get; set; } = default!;
    
    
    public void Setup()
    {
        BallLogic = new BallLogic();
        BallLogic.Set(this as IBall);
        BallLogic.Set(new BallLogic.Data());
        BallLogic.Set(GameRepo);
    }

    public void OnResolved()
    {
        BodyEntered += OnBodyEntered;
        
        BallBinding = BallLogic.Bind();

        BallBinding
            .Handle((in BallLogic.Output.PositionChanged output) =>
            {
                Position = output.Position;
            })
            .Handle((in BallLogic.Output.VelocityChanged output) =>
            {
                Velocity = output.Velocity;
            });
        
        this.Provide();
        
        BallLogic.Start();
    }

    public void OnReady()
    {
        SetPhysicsProcess(true);
    }

    public void OnExitTree()
    {
        BallLogic.Stop();
        BallBinding.Dispose();
    }

    public void OnPhysicsProcess(double delta)
    {
        BallLogic.Input(new BallLogic.Input.PhysicsTick(delta));
    }
    
    
    public void Enable()
    {
        BallLogic.Input(new BallLogic.Input.Enable());
    }
    
    public void Disable()
    {
        BallLogic.Input(new BallLogic.Input.Disable());
    }
    

    private void OnBodyEntered(Node2D body)
    {
        BallLogic.Input(new BallLogic.Input.Collision(body));
    }
}