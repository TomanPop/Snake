using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Snake body manager
/// </summary>
public class SnakeBodyManager : MonoBehaviour
{
    [SerializeField] private SnakeTail tailPrefab;

    private SnakeController snakeController;
    private SnakeTail tail;

    private void Start()
    {
        snakeController = GetComponent<SnakeController>();
        snakeController.SnakeMoved += OnSnakeMoved;
        
        tail = Instantiate(tailPrefab, transform.position, Quaternion.identity);
        tail.Initialize(null);
    }

    private void OnSnakeMoved(MapNode node, MoveDirection direction)
    {
        tail.Move(node, direction);
    }

    /// <summary>
    /// Grow snake
    /// </summary>
    public void GrowSnake()
    {
        tail.Grow();
    }

    /// <summary>
    /// Reduce snake
    /// </summary>
    public void ReduceSnake()
    {
        tail.Reduce();
    }

    /// <summary>
    /// Flip snake
    /// </summary>
    public void FlipSnake()
    {
        var tails = new List<SnakeTail>();
        var nodes = new List<MapNode>();
        var directions = new List<MoveDirection>();
        
        nodes.Add(snakeController.GetNode());
        directions.Add(snakeController.GetDirection());
        
        tail.GetTails(tails);

        //create tail map
        for(var i = 0; i < tails.Count; i++)
        {
            var switchTail = tails[i];
            nodes.Add(switchTail.GetCurrentNode());
            directions.Add(switchTail.GetCurrentDirection());
        }
        
        //adjust head
        var newDirection = directions[directions.Count-1];
        switch (newDirection)
        {
            case MoveDirection.Up:
                newDirection = MoveDirection.Down;
                break;
            case MoveDirection.Down:
                newDirection = MoveDirection.Up;
                break;
            case MoveDirection.Right:
                newDirection = MoveDirection.Left;
                break;
            case MoveDirection.Left:
                newDirection = MoveDirection.Right;
                break;
        }
        snakeController.SetStart(nodes[nodes.Count - 1], newDirection);
        
        //adjust tails
        for(var i = 0; i < tails.Count; i++)
        {
            var node = nodes[nodes.Count - 1 - i];
            var dir = directions[directions.Count-1 - i];
            tails[i].Reposition(node, dir);
        }
    }
}
