using Chickensoft.AutoInject;
using Chickensoft.Introspection;
using Godot;
using test.app.data;
using test.app.logic;
using test.common.utils.instantiator;
using test.game;

namespace test.app;

[Meta(typeof(IAutoNode))]
public partial class App : CanvasLayer, IApp
{
    public override void _Notification(int what) => this.Notify(what);
    
    #region MyRegion
    public IGame Game { get; private set; } = default!;
    #endregion

    #region Provisions
    IAppRepo IProvide<IAppRepo>.Value() => AppRepo;
    #endregion
    
    #region Nodes
    [Node] public AnimationPlayer SplashScreenAnimationPlayer { get; set; } = default!;
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