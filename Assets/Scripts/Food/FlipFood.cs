using UnityEngine;

/// <summary>
/// Flip food
/// </summary>
public class FlipFood : BaseFood
{
    public override void Eat()
    {
        base.Eat();
        
        var snakeBody = _snakeController.GetSnakeBody();
        var tail = snakeBody.GetTail();
        
        snakeBody.RevertBody(null);
        _snakeController.SetSnakeBody(tail);
    }
}
