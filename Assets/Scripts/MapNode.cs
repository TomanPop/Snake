using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Map node
/// </summary>
public class MapNode : MonoBehaviour, IMapNode
{
    public Vector2Int NodePosition { get; private set; }
    public IHitable Obstacle { get; set; }

    private Dictionary<MoveDirection, IMapNode> _neighbours;
    private Dictionary<IMapNode, MoveDirection> _directions;

    /// <summary>
    /// Initialize node
    /// </summary>
    /// <param name="map">parent map</param>
    /// <param name="nodePosition">node position/id</param>
    public void Initialize(Map map, Vector2Int nodePosition)
    {
        NodePosition = nodePosition;
        _neighbours = new Dictionary<MoveDirection, IMapNode>();
        _directions = new Dictionary<IMapNode, MoveDirection>();

        //mapping neighborhood
        var neighbourUp = map.GetNeighbourNode(NodePosition, MoveDirection.Up);
        var neighbourDown = map.GetNeighbourNode(NodePosition, MoveDirection.Down);
        var neighbourLeft = map.GetNeighbourNode(NodePosition, MoveDirection.Left);
        var neighbourRight = map.GetNeighbourNode(NodePosition, MoveDirection.Right);

        _neighbours.Add(MoveDirection.None, this);
        _neighbours.Add(MoveDirection.Up, neighbourUp);
        _neighbours.Add(MoveDirection.Down, neighbourDown);
        _neighbours.Add(MoveDirection.Left, neighbourLeft);
        _neighbours.Add(MoveDirection.Right, neighbourRight);

        _directions.Add(this, MoveDirection.None);
        _directions.Add(neighbourUp, MoveDirection.Up);
        _directions.Add(neighbourDown, MoveDirection.Down);
        _directions.Add(neighbourLeft, MoveDirection.Left);
        _directions.Add(neighbourRight, MoveDirection.Right);
    }

    /// <summary>
    /// Get neighbour node
    /// </summary>
    /// <param name="direction">neighbour direction</param>
    /// <returns>neighbour node in direction</returns>
    public IMapNode GetNeighbour(MoveDirection direction)
    {
        return _neighbours[direction];
    }

    public MoveDirection GetDirection(IMapNode neighbour)
    {
        return _directions[neighbour];
    }
}
