using Chickensoft.AutoInject;
using Chickensoft.GodotNodeInterfaces;
using Chickensoft.Introspection;
using Godot;
using test.app.data;
using test.app.logic;
using test.common.utils.instantiator;
using test.game;
using test.main_menu;

namespace test.app;

[Meta(typeof(IAutoNode))]
public partial class App : CanvasLayer, IApp
{
    public override void _Notification(int what) => this.Notify(what);
    
    #region MyRegion
    public IGame? Game { get; set; }
    public IMainMenu? MainMenu { get; set; }
    #endregion

    #region Provisions
    IAppRepo IProvide<IAppRepo>.Value() => AppRepo;
    #endregion
    
    #region Nodes
    [Node] public IControl SplashScreen { get; set; } = default!;
    [Node] public IAnimationPlayer SplashScreenAnimationPlayer { get; set; } = default!;
    #endregion
    
    #region Exports
    [Export] public PackedScene GameScene { get; set; } = default!;
    [Export] public PackedScene MainMenuScene { get; set; } = default!;
    [ExportGroup("AnimationNames")]
    [Export] public StringName SplashScreenFadeInAnimationName { get; set; } = default!;
    [Export] public StringName SplashScreenFadeOutAnimationName { get; set; } = default!;
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

        SplashScreenAnimationPlayer.AnimationFinished += OnSplashScreenAnimationFinished;
        
        this.Provide();
    }

    public void OnReady()
    {
        AppLogicBinding = AppLogic.Bind();

        AppLogicBinding
            .Handle((in AppLogic.Output.ShowSplashScreen _) =>
            {
                FadeInSplashScreen();
            })
            .Handle((in AppLogic.Output.HideSplashScreen _) =>
            {
                FadeOutSplashScreen();
            })
            .Handle((in AppLogic.Output.ShowMainMenu _) =>
            {
                if (MainMenu == null)
                {
                    MainMenu = Instantiator.Instantiate<MainMenu>(MainMenuScene);
                    AddChild((MainMenu)MainMenu);
                    // This is a workaround for the issue where the input handling order is not correct:
                    // https://forum.godotengine.org/t/change-input-handling-order-for-ui-elements/56601/5
                    MoveChild((Control) SplashScreen, -1);
                }
                
                Game?.Hide();
                MainMenu?.Show();
            });

        AppLogic.Start();
    }

    public void OnExitTree()
    {
        SplashScreenAnimationPlayer.AnimationFinished -= OnSplashScreenAnimationFinished;
    }


    private void FadeInSplashScreen()
    {
        SplashScreenAnimationPlayer.Play(SplashScreenFadeInAnimationName);
    }
    
    private void FadeOutSplashScreen()
    {
        SplashScreenAnimationPlayer.Play(SplashScreenFadeOutAnimationName);
    }
    
    
    private void OnSplashScreenAnimationFinished(StringName animationName)
    {
        if (animationName == SplashScreenFadeInAnimationName)
        {
            AppLogic.Input(new AppLogic.Input.SplashScreenFadedIn());
        } 
        else if (animationName == SplashScreenFadeOutAnimationName)
        {
            AppLogic.Input(new AppLogic.Input.SplashScreenFadedOut());
        } 
    }
}