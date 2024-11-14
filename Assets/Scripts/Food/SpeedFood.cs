using UnityEngine;

/// <summary>
/// Speed food
/// </summary>
public class SpeedFood : BaseFood
{
    [SerializeField] private SpeedBuff buffPrefab;
    
    public override void ApplyEffect(GameObject snake)
    {
        var buff = Instantiate(buffPrefab);
        var snakeController = snake.GetComponent<SnakeController>();
        snakeController.ApplyBuff(buff);
        
        base.ApplyEffect(snake);
    }
}