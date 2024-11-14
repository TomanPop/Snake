using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Snake tail
/// </summary>
public class SnakeTail : MonoBehaviour
{
    [SerializeField] private SnakeTail tailPrefab;

    private MapNode currentNode;
    private MoveDirection moveDirection;
    private SnakeTail childTail;
    private SnakeTail parentTail;

    /// <summary>
    /// Tail initialization
    /// </summary>
    /// <param name="parent">tail parent</param>
    public void Initialize(SnakeTail parent)
    {
        parentTail = parent;
    }

    /// <summary>
    /// Gets current node of tail
    /// </summary>
    /// <returns>node with snake tail</returns>
    public MapNode GetCurrentNode()
    {
        return currentNode;
    }
    
    /// <summary>
    /// Gets current direction of tail
    /// </summary>
    /// <returns></returns>
    public MoveDirection GetCurrentDirection()
    {
        return moveDirection;
    }

    /// <summary>
    /// Move tail
    /// </summary>
    /// <param name="mapNode">target node</param>
    /// <param name="direction"></param>
    public void Move(MapNode mapNode, MoveDirection direction)
    {
        if(childTail != null)
            childTail.Move(currentNode, moveDirection);
        else if(currentNode != null)
            currentNode.IsObstacle = false;
        
        mapNode.IsObstacle = true;
        transform.position = new Vector3(mapNode.NodePosition.x, 0, mapNode.NodePosition.y);
        currentNode = mapNode;
        moveDirection = direction;
    }

    /// <summary>
    /// Grow tail
    /// </summary>
    public void Grow()
    {
        if (childTail == null)
        {
            childTail = Instantiate(tailPrefab, transform.position, Quaternion.identity);
            childTail.Initialize(this);
            childTail.name = "ChildTail";
        }
        else
        {
            childTail.Grow();
        }
    }

    /// <summary>
    /// Reduce tail
    /// </summary>
    public void Reduce()
    {
        if (childTail != null)
        {
            childTail.Reduce();
            return;
        }
        
        if (parentTail == null)
            return;
        
        currentNode.IsObstacle = false;
        Destroy(gameObject);
    }

    /// <summary>
    /// construct tail map
    /// </summary>
    /// <param name="tails">all tails</param>
    public void GetTails(List<SnakeTail> tails)
    {
        tails.Add(this);
        childTail?.GetTails(tails);
    }
}
