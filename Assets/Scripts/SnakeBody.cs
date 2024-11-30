using UnityEngine;

public class SnakeBody : MonoBehaviour, IBody, IDestroyable
{
    private IMapNode _currentNode;
    private IMapNode _previousNode;
    private IBody _childBody;
    private MoveDirection _moveDirection;

    /// <summary>
    /// Body initialization
    /// </summary>
    /// <param name="parent">body parent</param>
    /// <param name="node"></param>
    public void Initialize(IMapNode node)
    {
        _currentNode = _previousNode = node;
        UpdatePosition();
    }

    public void AddBodyPart(IBody child)
    {
        if (_childBody == null)
            _childBody = child;
        else
            _childBody.AddBodyPart(child);
    }

    public void RemoveBodyPart(IBody parent, ISnakeFactory factory)
    {
        if (_childBody != null)
        {
            _childBody.RemoveBodyPart(this, factory);
        }
        else if(parent != null)
        {
            parent.ClearChild();
            factory.DestroySnakeBodyPart(this);
        }
    }

    public void RevertBody(IBody parent)
    {
        _childBody?.RevertBody(this);
        _childBody = parent;
        
        _moveDirection = _currentNode.GetDirection(_previousNode);
    }

    public IBody GetTail()
    {
        return _childBody == null ? this : _childBody.GetTail();
    }

    public IBody GetChild()
    {
        return _childBody;
    }

    public void ClearChild()
    {
        _childBody = null;
    }

    public void CleanUp()
    {
        if (_currentNode.Obstacle != null && _currentNode.Obstacle == this)
            _currentNode.Obstacle = null;

        _currentNode = null;
        _previousNode = null;
        _childBody = null;
    }

    public IMapNode GetCurrentNode()
    {
        return _currentNode;
    }
    
    public IMapNode GetPreviousNode()
    {
        return _previousNode;
    }

    public MoveDirection GetMoveDirection()
    {
        return _moveDirection;//_previousNode.GetDirection(_currentNode);
    }

    /// <summary>
    /// Move tail
    /// </summary>
    /// <param name="targetNode">target node</param>
    public void Move(IMapNode targetNode)
    {
        _previousNode = _currentNode;
        _currentNode = targetNode;
        _currentNode.Obstacle = this;
        _previousNode.Obstacle = null;

        _moveDirection = _previousNode.GetDirection(_currentNode);
        
        UpdatePosition();

        if (_childBody != null)
            _childBody.Move(_previousNode);
    }

    private void UpdatePosition()
    {
        transform.position = new Vector3(_currentNode.NodePosition.x, 0, _currentNode.NodePosition.y);
    }
}