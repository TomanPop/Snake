/// <summary>
/// Reduce food
/// </summary>
public class ReduceFood : BaseFood
{
    private ISnakeFactory _snakeFactory;

    public void Initialize(ISnakeFactory snakeFactory, ISnakeController snakeController, IMapNode node)
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
