using UnityEngine;

/// <summary>
/// Speed buff
/// </summary>
public class SpeedBuff : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float effectTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyEffect", effectTime);
    }

    private void DestroyEffect()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Gets speed buff
    /// </summary>
    /// <returns>speed buff</returns>
    public float GetBuff()
    {
        return speedMultiplier;
    }
}
