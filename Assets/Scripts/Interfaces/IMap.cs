using UnityEngine;
public interface IMap
{
    public IMapNode GetNeighbourNode(Vector2Int nodePosition, MoveDirection direction);
    public IMapNode GetNode(Vector2Int vector2Int);
    public bool TryGetFreeNode(out IMapNode node);
}