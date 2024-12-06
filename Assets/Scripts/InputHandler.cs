using System;
using UnityEngine;
using Zenject;

/// <summary>
/// Snake controller
/// </summary>
public class InputHandler : MonoBehaviour
{
    private ISnakeController _snakeController;
    private ICommandInvoker _commandInvoker;
    private IUIController _uiController;

    [Inject]
    public void Initialize(ISnakeController snakeController, IUIController uiController, ICommandInvoker commandInvoker)
    {
        _uiController = uiController;
        _snakeController = snakeController;
        _commandInvoker = commandInvoker;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var command = new ShowHideMenuCommand(_uiController);
            _commandInvoker.ExecuteCommand(command);
        }

        var moveDirection = UpdateInput();
        if (moveDirection != MoveDirection.None)
            OnInputChanged(moveDirection);
    }

    private MoveDirection UpdateInput()
    {
        //using horizontal and vertical so its usable on joystick too
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        
        if (x > 0)
        {
            return MoveDirection.Right;
        }
        
        if (x < 0)
        {
            return MoveDirection.Left;
        }

        if (y > 0)
        {
            return MoveDirection.Up;
        }
        
        if (y < 0)
        {
            return MoveDirection.Down;
        }
        
        return MoveDirection.None;
    }

    private void OnInputChanged(MoveDirection moveDirection)
    {
        var command = new MoveCommand(_snakeController, moveDirection);
        _commandInvoker.ExecuteCommand(command);
    }
}
