using System;
using UnityEngine;

/// <summary>
/// Base food
/// </summary>
public class BaseFood : MonoBehaviour
{
    [SerializeField] private int score;
    
    public event Action<int> FoodEaten;
    
    /// <summary>
    /// Apply food effect
    /// </summary>
    /// <param name="snake">snake on which is effect applied</param>
    public virtual void ApplyEffect(GameObject snake)
    {
        FoodEaten?.Invoke(score);
    }
}
