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
    #endregion

    #region Provisions
    IAppRepo IProvide<IAppRepo>.Value() => AppRepo;
    #endregion
    
    #region Nodes
    [Node] public IControl SplashScreen { get; set; } = default!;
    [Node] public IAnimationPlayer SplashScreenAnimationPlayer { get; set; } = default!;
    [Node] public IMainMenu MainMenu { get; set; } = default!;
    #endregion
    
    #region Exports
    [Export] public PackedScene GameScene { get; set; } = default!;
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
        MainMenu.GameStarted += OnGameStarted;
        
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
                Game?.Hide();
                MainMenu.Show();
            })
            .Handle((in AppLogic.Output.ShowGame _) =>
            {
                Game = Instantiator.Instantiate<Game>(GameScene);
                AddChild((Game) Game);
                
                Game?.Show();
                MainMenu.Hide();
                SplashScreen.Hide();
            });

        AppLogic.Start();
    }

    public void OnExitTree()
    {
        SplashScreenAnimationPlayer.AnimationFinished -= OnSplashScreenAnimationFinished;
        MainMenu.GameStarted -= OnGameStarted;
    }


    private void FadeInSplashScreen()
    {
        SplashScreen.Show();
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
            SplashScreen.Hide();
            AppLogic.Input(new AppLogic.Input.SplashScreenFadedOut());
        } 
    }
    
    private void OnGameStarted()
    {
        AppLogic.Input(new AppLogic.Input.LoadGame());
    }
}