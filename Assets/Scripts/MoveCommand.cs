public class MoveCommand : ICommand
{
    private ISnakeController _snakeController;
    private MoveDirection _moveDirection;
    
    public MoveCommand(ISnakeController snakeController, MoveDirection moveDirection)
    {
        _snakeController = snakeController;
        _moveDirection = moveDirection;
    }

    public void Execute()
    {
        _snakeController.Move(_moveDirection);
    }
}