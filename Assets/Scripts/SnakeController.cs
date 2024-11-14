using System;
using UnityEngine;

/// <summary>
/// Snake controller
/// </summary>
public class SnakeController : MonoBehaviour
{
    [SerializeField] private float snakeBaseTimeStep = 0.5f;

    private SpeedBuff speedBuff;
    private GameManager gameManager;
    private MapNode currentNode;
    private float nextStep;
    private bool isAlive;
    private MoveDirection currentMoveDirection = MoveDirection.Right;
    private MoveDirection nextMoveDirection = MoveDirection.Right;

    public event Action<MapNode, MoveDirection> SnakeMoved;
    
    void Start()
    {
        isAlive = true;
        gameManager = FindObjectOfType<GameManager>();
        nextStep = Time.time + snakeBaseTimeStep;

        var map = FindObjectOfType<Map>();
        if(map.TryGetFreeNode(out var node))
            SetStart(node, MoveDirection.Right);
    }
    
    void Update()
    {
        if (!isAlive)
            return;
        
        var isInput = UpdateInput();

        if (isInput || Time.time > nextStep)
            Step();
    }

    /// <summary>
    /// Used for buff application
    /// </summary>
    /// <param name="buff">buff to apply</param>
    public void ApplyBuff(SpeedBuff buff)
    {
        speedBuff = buff;
    }

    public void SetStart(MapNode node, MoveDirection direction)
    {
        currentNode = node;
        currentMoveDirection = nextMoveDirection = direction;
        
        Step();
    }

    public MapNode GetNode()
    {
        return currentNode;
    }

    public MoveDirection GetDirection()
    {
        return currentMoveDirection;
    }

    private bool UpdateInput()
    {
        //using horizontal and vertical so its usable on joystick too
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        
        Input.ResetInputAxes();

        if (x > 0 && currentMoveDirection != MoveDirection.Left && currentMoveDirection != MoveDirection.Right)
        {
            nextMoveDirection = MoveDirection.Right;
            return true;
        }
        
        if (x < 0 && currentMoveDirection != MoveDirection.Left && currentMoveDirection != MoveDirection.Right)
        {
            nextMoveDirection = MoveDirection.Left;
            return true;
        }
        
        if (y > 0 && currentMoveDirection != MoveDirection.Up && currentMoveDirection != MoveDirection.Down)
        {
            nextMoveDirection = MoveDirection.Up;
            return true;
        }
        
        if (y < 0 && currentMoveDirection != MoveDirection.Up && currentMoveDirection != MoveDirection.Down)
        {
            nextMoveDirection = MoveDirection.Down;
            return true;
        }
        
        return false;
    }

    private void Step()
    {
        var newNode = currentNode.GetNeighbour(nextMoveDirection);
        transform.position = new Vector3(newNode.NodePosition.x, 0, newNode.NodePosition.y);
        
        SnakeMoved?.Invoke(currentNode, nextMoveDirection);

        //crashed
        if (newNode.IsObstacle)
        {
            isAlive = false;
            gameManager.GameOver();
            return;
        }

        currentNode = newNode;
        currentMoveDirection = nextMoveDirection;

        var buff = speedBuff != null ? speedBuff.GetBuff() : 1;
        nextStep = Time.time + snakeBaseTimeStep * buff;
    }
}
