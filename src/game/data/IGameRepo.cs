using System;
using test.lib.auto_prop;

namespace test.game.data;

public interface IGameRepo
{
    /// <summary>
    /// The game has started.
    /// </summary>
    event Action? GameStarted;
    /// <summary>
    /// The game is over due to the reason is provided.
    /// </summary>
    event Action<GameEndedReason>? GameEnded;
    /// <summary>
    /// The ball has bounced off of something.
    /// </summary>
    event Action<BallCollidedAction>? BallCollided;
    /// <summary>
    /// The ball has been reset.
    /// </summary>
    event Action? BallReset;
    
    IAutoProp<uint> LeftScore { get; }
    IAutoProp<uint> RightScore { get; }
    IAutoProp<bool> IsPaused { get; }
    
    
    /// <summary>
    /// The game has started.
    /// </summary>
    void OnGameStarted();
    /// <summary>
    /// The game is over due to the reason is provided.
    /// </summary>
    /// <param name="reason"></param>
    void OnGameEnded(GameEndedReason reason);
    /// <summary>
    /// The ball has bounced off of something.
    /// </summary>
    /// <param name="action">What the ball bounced off of</param>
    void OnBallCollided(BallCollidedAction action);
    /// <summary>
    /// The ball has been reset.
    /// </summary>
    void OnBallReset();
    /// <summary>
    /// Increment the left score.
    /// </summary>
    void IncrementLeftScore();
    /// <summary>
    /// Increment the right score.
    /// </summary>
    void IncrementRightScore();
    /// <summary>
    /// Pause the game.
    /// </summary>
    void Pause();
    /// <summary>
    /// Resume the game.
    /// </summary>
    void Resume();
}