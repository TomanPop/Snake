using UnityEngine;

/// <summary>
/// Flip food
/// </summary>
public class FlipFood : BaseFood
{
    public override void ApplyEffect(GameObject snake)
    {
        var snakeBodyManager = snake.GetComponent<SnakeBodyManager>();
        snakeBodyManager.FlipSnake();
        
        base.ApplyEffect(snake);
    }
}
