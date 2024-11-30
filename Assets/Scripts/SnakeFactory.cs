using System.Collections.Generic;
using UnityEngine;

public class SnakeFactory : MonoBehaviour
{
    [SerializeField] private SnakeBody snakeBodyPrefab;

    private readonly Stack<SnakeBody> _pool = new();

    public SnakeBody CreateSnakeBodyPart(MapNode node)
    {
        var snakeBody = GetBodyPart();
        snakeBody.Initialize(node);
        
        return snakeBody;
    }

    private SnakeBody GetBodyPart()
    {
        if (_pool.TryPop(out var bodyPart))
        {
            bodyPart.gameObject.SetActive(true);
            return bodyPart;
        }

        return Instantiate(snakeBodyPrefab);
    }

    public void DestroySnakeBodyPart(IDestroyable bodyPart)
    {
        if (bodyPart is not SnakeBody snakeBody)
            return;
        
        snakeBody.CleanUp();
        snakeBody.gameObject.SetActive(false);
        _pool.Push(snakeBody);
    }
}