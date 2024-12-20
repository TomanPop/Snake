using UnityEngine;

/// <summary>
/// Speed buff
/// </summary>
public class SpeedBuff : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float effectTime;

    private ISnakeController _snakeController;

    public void Initialize(ISnakeController snakeController)
    {
        _snakeController = snakeController;
        _snakeController.RegisterBuff(this);
        
        Invoke("DestroyEffect", effectTime);
    }

    public float GetSpeedAdjustment()
    {
        return speedMultiplier;
    }

    private void DestroyEffect()
    {
        _snakeController.UnregisterBuff(this);
        Destroy(gameObject);
    }
}
