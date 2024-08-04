using System;

namespace test.app.data;

public interface IAppRepo
{
    /// <summary>
    /// Event that is triggered when the splash screen is skipped.
    /// </summary>
    public event Action? SplashScreenSkipped;
    /// <summary>
    /// Event that is triggered when the game starts.
    /// </summary>
    event Action? GameStarted;
    /// <summary>
    /// Event that is triggered when the game ends.
    /// The action which should be taken after the game ends is provided.
    /// </summary>
    event Action<GameEndedAction>? GameEnded;
    
    
    /// <summary>
    /// Notify that the splash screen was skipped.
    /// </summary>
    void OnSplashScreenSkipped();
    /// <summary>
    /// Notify that the game should start.
    /// </summary>
    void OnGameStarted();
    /// <summary>
    /// Notify that the game should end.
    /// </summary>
    /// <param name="action">Action to take after the game has ended</param>
    void OnGameEnded(GameEndedAction action);
}