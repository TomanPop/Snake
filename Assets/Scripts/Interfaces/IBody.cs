using System.Collections.Generic;

public interface IBody : IHitable
{
    public MoveDirection GetMoveDirection();
    public IMapNode GetCurrentNode();
    public void AddBodyPart(IBody child);
    public void RemoveBodyPart(IBody parent, ISnakeFactory factory);
    public void ClearChild();
    public void Move(IMapNode targetNode);
    public IBody GetTail();
    public IBody GetChild();
    public void RevertBody(IBody parent);

}