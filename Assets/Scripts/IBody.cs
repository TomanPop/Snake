using System.Collections.Generic;

public interface IBody : IHitable
{
    public MoveDirection GetMoveDirection();
    public MapNode GetCurrentNode();
    public MapNode GetPreviousNode();
    public void AddBodyPart(IBody child);
    public void RemoveBodyPart(IBody parent, SnakeFactory factory);
    public void ClearChild();
    public void Move(MapNode targetNode);
    public IBody GetTail();
    public IBody GetChild();
    public void RevertBody(IBody parent);

}