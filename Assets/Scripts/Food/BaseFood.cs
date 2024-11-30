using System;
using UnityEngine;

/// <summary>
/// Base food
/// </summary>
public class BaseFood : MonoBehaviour, IFood
{
    [SerializeField] private int score;

    protected MapNode _node;
    protected SnakeController _snakeController;
    public event Action<int> FoodEaten;

    public void Initialize(SnakeController snakeController, MapNode node)
    {
        _snakeController = snakeController;
        _node = node;

        _node.Obstacle = this;
        
        var position = new Vector3(node.NodePosition.x, 0, node.NodePosition.y);
        transform.position = position;
    }

    /// <summary>
    /// Apply food effect
    /// </summary>
    public virtual void Eat()
    {
        if(_node.Obstacle == this)
            _node.Obstacle = null;
        
        FoodEaten?.Invoke(score);
    }
}
