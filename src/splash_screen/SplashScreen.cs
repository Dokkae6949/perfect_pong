using Chickensoft.AutoInject;
using Chickensoft.Introspection;
using Godot;
using test.app.data;
using test.splash_screen;

[Meta(typeof(IAutoNode))]
public partial class SplashScreen : Control, ISplashScreen
{
    public override void _Notification(int what) => this.Notify(what);

    #region Dependencies
    [Dependency] public IAppRepo AppRepo => this.DependOn<IAppRepo>();
    #endregion
    
    
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey { Pressed: true, Keycode: Key.Space })
        {
            AppRepo.OnSplashScreenSkipped();
        }
    }
}
