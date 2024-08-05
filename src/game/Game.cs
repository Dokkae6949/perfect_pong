using Chickensoft.AutoInject;
using Chickensoft.Introspection;
using Godot;
using test.app.data;
using test.ball;
using test.common.utils.instantiator;
using test.game.data;
using test.game.logic;

namespace test.game;

[Meta(typeof(IAutoNode))]
public partial class Game : Node2D, IGame
{
    public override void _Notification(int what) => this.Notify(what);

    #region Provisions
    IGameRepo IProvide<IGameRepo>.Value() => GameRepo;
    #endregion
    
    #region Dependencies
    [Dependency] public IAppRepo AppRepo => this.DependOn<IAppRepo>();
    #endregion

    #region External
    public IInstantiator Instantiator { get; set; } = default!;
    #endregion
    
    #region Exports
    [Export] public PackedScene? BallScene { get; set; }
    #endregion

    #region References
    public IBall? Ball { get; set; }
    #endregion
    
    public IGameRepo GameRepo { get; set; } = default!;
    public IGameLogic GameLogic { get; set; } = default!;
    public GameLogic.IBinding GameLogicBinding { get; set; } = default!;


    public void Setup()
    {
        Instantiator = new Instantiator(GetTree());
        GameRepo = new GameRepo();
        GameLogic = new GameLogic();
        GameLogic.Set(GameRepo);
        GameLogic.Set(AppRepo);
    }

    public void OnResolved()
    {
        GD.Print("Dependencies resolved.");
        GameLogicBinding = GameLogic.Bind();

        GameLogicBinding
            .Handle((in GameLogic.Output.StartGame _) =>
            {
                GD.Print("Initializing game.");
                if (BallScene == null)
                {
                    GD.PrintErr("BallScene is not set. Cannot start game.");
                }
                
                Ball = Instantiator.Instantiate<Ball>(BallScene!);
                Ball.Position = GetViewportRect().Size / 2;
                Ball.Velocity = new Vector2(200f * (GD.Randf() > 0.5 ? 1f : -1f), 150f * (float) GD.RandRange(-1.0, 1.0));
                AddChild((Ball) Ball);
            });
        
        GameLogic.Start();
        
        this.Provide();
    }
}