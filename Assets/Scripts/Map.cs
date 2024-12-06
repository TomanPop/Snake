using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Map
/// </summary>
public class Map : MonoBehaviour, IMap
{
    [SerializeField] private MapNode mapNodePrefab;

    private Vector2Int mapSize;
    private Dictionary<Vector2Int, IMapNode> mapNodes;

    private const float MaxOffsetY = 0.1f;
    private const int MaxAttemptCount = 50;
    
    [Inject]
    public void Initialize(IAppSettingsService appSettingsService)
    {
        mapSize = new Vector2Int(appSettingsService.GameSettings.MapSizeX,
            appSettingsService.GameSettings.MapSizeY);
        mapNodes = new Dictionary<Vector2Int, IMapNode>();
        
        GenerateMap();
    }

    private void GenerateMap()
    {
        //prepare all nodes
        for (var x = 0; x < mapSize.x; x++)
        {
            for (var y = 0; y < mapSize.y; y++)
            {
                var nodeId = new Vector2Int(x, y);
                var yOffset = Random.Range(-MaxOffsetY, MaxOffsetY);
                var node = Instantiate(mapNodePrefab, new Vector3(x,yOffset, y), Quaternion.identity, transform);
                mapNodes.Add(nodeId, node);
            }
        }

        //Initialize node after all nodes are prepared
        foreach (var mapNode in mapNodes)
        {
            var node = mapNode.Value as MapNode;
            node.Initialize(this, mapNode.Key);
        }
    }

    /// <summary>
    /// Get neighbour node for position and direction
    /// </summary>
    /// <param name="nodePosition">node position</param>
    /// <param name="direction">neighbour direction</param>
    /// <returns>neighbour node</returns>
    public IMapNode GetNeighbourNode(Vector2Int nodePosition, MoveDirection direction)
    {
        var x = nodePosition.x;
        var y = nodePosition.y;
        
        switch (direction)
        {
            case MoveDirection.Up:
                y = y == mapSize.y - 1 ? 0 : (y + 1);
                break;
            case MoveDirection.Down:
                y = y == 0 ? mapSize.y - 1 : (y - 1);
                break;
            case MoveDirection.Right:
                x = x == mapSize.x - 1 ? 0 : (x + 1);
                break;
            case MoveDirection.Left:
                x = x == 0 ? mapSize.x - 1 : (x - 1);
                break;
        }

        return mapNodes[new Vector2Int(x, y)];
    }

    /// <summary>
    /// Get node with id/position
    /// </summary>
    /// <param name="vector2Int">node id</param>
    /// <returns>node with specified id</returns>
    public IMapNode GetNode(Vector2Int vector2Int)
    {
        return mapNodes[vector2Int];
    }

    public bool TryGetFreeNode(out IMapNode node)
    {
        var attempt = 0;
        
        //try random pick
        while (attempt < MaxAttemptCount)
        {
            attempt++;
            
            var nodeId = new Vector2Int(Random.Range(0, mapSize.x), Random.Range(0, mapSize.y));
            var nodeCheck = mapNodes[nodeId];

            if (nodeCheck.Obstacle != null)
                continue;

            node = nodeCheck;
            return true;
        }
        
        //try sequential pick
        for (var x = 0; x < mapSize.x; x++)
        {
            for (var y = 0; y < mapSize.y; y++)
            {
                var nodeId = new Vector2Int(x, y);
                var nodeCheck = mapNodes[nodeId];

                if (nodeCheck.Obstacle == null)
                {
                    node = nodeCheck;
                    return true;
                }
            }
        }
        
        //everything is full
        node = null;
        return false;
    }
}
