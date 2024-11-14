using UnityEngine;

/// <summary>
/// Snake eater
/// </summary>
public class SnakeEater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var food = other.transform.root.GetComponent<BaseFood>();
        food?.ApplyEffect(gameObject);
    }
}
