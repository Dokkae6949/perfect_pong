using System;
using test.lib.auto_prop;

namespace test.game.data;

public class GameRepo : IGameRepo
{
    public event Action? GameStarted;
    public event Action<GameEndedReason>? GameEnded;
    public event Action<BallCollidedAction>? BallCollided;
    public event Action? BallReset;

    public IAutoProp<uint> LeftScore => _leftScore;
    private readonly AutoProp<uint> _leftScore = new(0);
    public IAutoProp<uint> RightScore => _rightScore;
    private readonly AutoProp<uint> _rightScore = new(0);
    public IAutoProp<uint> MaxScore => _maxScore;
    private readonly AutoProp<uint> _maxScore = new(0);
    public IAutoProp<bool> IsPaused => _isPaused;
    private readonly AutoProp<bool> _isPaused = new(false);
    
    
    public void OnBallReset() => BallReset?.Invoke();
    public void OnGameStarted() => GameStarted?.Invoke();
    public void OnBallCollided(BallCollidedAction action) => BallCollided?.Invoke(action);
    public void OnGameEnded(GameEndedReason reason) => GameEnded?.Invoke(reason);
    public void IncrementLeftScore()
    {
        _leftScore.OnChanged(score => score + 1);
        
        if (_leftScore.Value >= _maxScore.Value)
        {
            OnGameEnded(GameEndedReason.LeftWon);
        }
    }
    public void IncrementRightScore()
    {
        _rightScore.OnChanged(score => score + 1);
        
        if (_rightScore.Value >= _maxScore.Value)
        {
            OnGameEnded(GameEndedReason.RightWon);
        }
    }
    public void Pause() => _isPaused.OnChanged(true);
    public void Resume() => _isPaused.OnChanged(false);
}