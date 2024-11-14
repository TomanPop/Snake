using UnityEngine;

/// <summary>
/// Reduce food
/// </summary>
public class ReduceFood : BaseFood
{
    public override void ApplyEffect(GameObject snake)
    {
        var snakeBodyManager = snake.GetComponent<SnakeBodyManager>();
        snakeBodyManager.ReduceSnake();
        
        base.ApplyEffect(snake);
    }
}
