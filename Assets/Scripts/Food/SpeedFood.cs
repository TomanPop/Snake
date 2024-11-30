using UnityEngine;

/// <summary>
/// Speed food
/// </summary>
public class SpeedFood : BaseFood
{
    [SerializeField] private SpeedBuff buffPrefab;
    
    public override void Eat()
    {
        base.Eat();
        var buff = Instantiate(buffPrefab);
        buff.Initialize(_snakeController);
    }
}