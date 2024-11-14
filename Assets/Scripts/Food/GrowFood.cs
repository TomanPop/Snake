using UnityEngine;

/// <summary>
/// Grow food
/// </summary>
public class GrowFood : BaseFood
{
    public override void ApplyEffect(GameObject snake)
    {
        var snakeBodyManager = snake.GetComponent<SnakeBodyManager>();
        snakeBodyManager.GrowSnake();
        
        base.ApplyEffect(snake);
    }
}
