using UnityEngine;

public interface IMapNode
{
    public Vector2Int NodePosition { get; }
    public IHitable Obstacle { get; set; }
    public IMapNode GetNeighbour(MoveDirection direction);
    public MoveDirection GetDirection(IMapNode neighbour);
}