using Chickensoft.AutoInject;
using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;
using Godot;
using test.app.data;
using test.app.logic;
using test.common.utils.instantiator;
using test.game;

namespace test.app;

[Meta(typeof(IAutoNode))]
public partial class App : Node, IApp
{
    public override void _Notification(int what) => this.Notify(what);

    #region MyRegion
    public IGame Game { get; private set; } = default!;
    #endregion

    #region Provisions
    IAppRepo IProvide<IAppRepo>.Value() => AppRepo;
    #endregion
    
    #region Nodes
    // TODO: Add splash screen and main menu
    #endregion
    
    #region Exports
    [Export] public PackedScene GameScene { get; set; } = default!;
    #endregion
    
    public IInstantiator Instantiator { get; set; } = default!;
    public IAppRepo AppRepo { get; set; } = default!;
    public IAppLogic AppLogic { get; set; } = default!;
    public AppLogic.IBinding AppLogicBinding { get; set; } = default!;


    public void Initialize()
    {
        Instantiator = new Instantiator(GetTree());
        AppRepo = new AppRepo();
        AppLogic = new AppLogic();
        AppLogic.Set(AppRepo);
        
        this.Provide();
    }

    public void OnReady()
    {
        AppLogicBinding = AppLogic.Bind();

        AppLogicBinding
            .Handle((in logic.AppLogic.Output.ShowSplashScreen _) =>
            {

            })
            .Handle((in logic.AppLogic.Output.HideSplashScreen _) =>
            {

            });

        // TODO: Handle states
    }
}