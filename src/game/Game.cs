using Chickensoft.AutoInject;
using Chickensoft.Introspection;
using Godot;
using test.ball;
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
    
    #region Nodes
    
    [Node] public IBall Ball { get; set; } = default!;
    
    #endregion
    
    public IGameRepo GameRepo { get; set; } = default!;
    public IGameLogic GameLogic { get; set; } = default!;
    public GameLogic.IBinding GameLogicBinding { get; set; } = default!;


    public void Setup()
    {
        GameRepo = new GameRepo();
        GameLogic = new GameLogic();
    }

    public void OnResolved()
    {
        GameLogicBinding = GameLogic.Bind();
        
        this.Provide();
    }
}