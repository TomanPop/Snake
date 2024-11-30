using UnityEngine;

/// <summary>
/// Grow food
/// </summary>
public class GrowFood : BaseFood
{
    private SnakeFactory _snakeFactory;

    public void Initialize(SnakeFactory snakeFactory, SnakeController snakeController, MapNode node)
    {
        _snakeFactory = snakeFactory;
        base.Initialize(snakeController, node);
    }
    
    public override void Eat()
    {
        base.Eat();
        
        var head = _snakeController.GetSnakeBody();
        var tail = head.GetTail();
        var body = _snakeFactory.CreateSnakeBodyPart(tail.GetCurrentNode());
        
        tail.AddBodyPart(body);
    }
}
