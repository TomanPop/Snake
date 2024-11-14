using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Map node
/// </summary>
public class MapNode : MonoBehaviour
{
    public Vector2Int NodePosition { get; private set; }
    public bool IsObstacle { get; set; }

    private Dictionary<MoveDirection, MapNode> neighbours;

    /// <summary>
    /// Initialize node
    /// </summary>
    /// <param name="map">parent map</param>
    /// <param name="nodePosition">node position/id</param>
    public void Initialize(Map map, Vector2Int nodePosition)
    {
        NodePosition = nodePosition;
        neighbours = new Dictionary<MoveDirection, MapNode>();

        //mapping neighborhood
        var neighbourUp = map.GetNeighbourNode(NodePosition, MoveDirection.Up);
        var neighbourDown = map.GetNeighbourNode(NodePosition, MoveDirection.Down);
        var neighbourLeft = map.GetNeighbourNode(NodePosition, MoveDirection.Left);
        var neighbourRight = map.GetNeighbourNode(NodePosition, MoveDirection.Right);
        
        neighbours.Add(MoveDirection.Up, neighbourUp);
        neighbours.Add(MoveDirection.Down, neighbourDown);
        neighbours.Add(MoveDirection.Left, neighbourLeft);
        neighbours.Add(MoveDirection.Right, neighbourRight);
    }

    /// <summary>
    /// Get neighbour node
    /// </summary>
    /// <param name="direction">neighbour direction</param>
    /// <returns>neighbour node in direction</returns>
    public MapNode GetNeighbour(MoveDirection direction)
    {
        return neighbours[direction];
    }
}
