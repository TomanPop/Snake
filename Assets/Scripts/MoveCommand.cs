public class MoveCommand : ICommand
{
    private SnakeController _snakeController;
    private MoveDirection _moveDirection;
    
    public MoveCommand(SnakeController snakeController, MoveDirection moveDirection)
    {
        _snakeController = snakeController;
        _moveDirection = moveDirection;
    }

    public void Execute()
    {
        _snakeController.Move(_moveDirection);
    }
}