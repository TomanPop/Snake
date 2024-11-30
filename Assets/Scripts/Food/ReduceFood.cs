/// <summary>
/// Reduce food
/// </summary>
public class ReduceFood : BaseFood
{
    private SnakeFactory _snakeFactory;

    public void Initialize(SnakeFactory snakeFactory, SnakeController snakeController, MapNode node)
    {
        _snakeFactory = snakeFactory;
        base.Initialize(snakeController, node);
    }
    
    public override void Eat()
    {
        var snakeBody = _snakeController.GetSnakeBody();
        snakeBody.RemoveBodyPart(null, _snakeFactory);
        
        base.Eat();
    }
}
