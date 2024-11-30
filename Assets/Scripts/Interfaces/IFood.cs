using System;
using UnityEngine;

public interface IFood : IHitable
{
    public event Action<int> FoodEaten;

    public void Eat();
}