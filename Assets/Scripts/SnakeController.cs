using UnityEngine;

/// <summary>
/// Snake body manager
/// </summary>
public class SnakeController : MonoBehaviour
{
    private float _snakeBaseTimeStep;
    private IBody _snakeBody;
    private GameManager _gameManager;
    private SpeedBuff _speedBuff;

    private float _nextStep;
    private bool _isAlive = true;

    public void Initialize(GameManager gameManager, IBody snakeBody, IAppSettingsService appSettingsService)
    {
        _gameManager = gameManager;
        _snakeBody = snakeBody;

        _snakeBaseTimeStep = appSettingsService.GameSettings.SnakeBaseTimeStep;
        Move(GameContext.BeginDirection);
    }

    private void LateUpdate()
    {
        if (Time.time > _nextStep)
            MoveSnake();
    }

    private void MoveSnake()
    {
        var moveDirection = _snakeBody.GetMoveDirection();
        MoveSnake(moveDirection);
    }
    
    public void Move(MoveDirection moveDirection)
    {
        if (!ValidateInput(moveDirection))
            return;
        
        MoveSnake(moveDirection);
    }

    private void MoveSnake(MoveDirection moveDirection)
    {
        var newNode = _snakeBody.GetCurrentNode().GetNeighbour(moveDirection);
        _nextStep = Time.time + _snakeBaseTimeStep * (_speedBuff != null ? _speedBuff.GetSpeedAdjustment() : 1);
        
        Move(newNode);
    }

    private bool ValidateInput(MoveDirection moveDirection)
    {
        var currentDirection = _snakeBody.GetMoveDirection();

        if (moveDirection == currentDirection)
            return false;
        
        if (moveDirection == MoveDirection.Down && currentDirection == MoveDirection.Up)
            return false;
        
        if (moveDirection == MoveDirection.Up && currentDirection == MoveDirection.Down)
            return false;
        
        if (moveDirection == MoveDirection.Left && currentDirection == MoveDirection.Right)
            return false;
        
        if (moveDirection == MoveDirection.Right && currentDirection == MoveDirection.Left)
            return false;

        return true;
    }

    private void Move(MapNode node)
    {
        var obstacle = node.Obstacle;
        if (obstacle  != null)
        {
            switch (obstacle)
            {
                case IFood food:
                    _snakeBody.Move(node);
                    food.Eat();
                    return;
                case IBody body:
                    _isAlive = false;
                    _gameManager.GameOver();
                    return;
            }
        }
        
        _snakeBody.Move(node);
    }

    public IBody GetSnakeBody()
    {
        return _snakeBody;
    }

    public void SetSnakeBody(IBody body)
    {
        _snakeBody = body;
    }

    public void UnregisterBuff(SpeedBuff speedBuff)
    {
        _speedBuff = speedBuff;
    }

    public void RegisterBuff(SpeedBuff speedBuff)
    {
        if (speedBuff != _speedBuff)
            return;

        _speedBuff = null;
    }
}
