using System;

namespace test.app.data;

public class AppRepo : IAppRepo
{
    public event Action? SplashScreenSkipped;
    public event Action? GameStarted;
    public event Action<GameEndedAction>? GameEnded;
    
    
    public void OnSplashScreenSkipped() => SplashScreenSkipped?.Invoke();
    public void OnGameStarted() => GameStarted?.Invoke();
    public void OnGameEnded(GameEndedAction action) => GameEnded?.Invoke(action);
}