using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class FoodFactory : MonoBehaviour, IFoodFactory
{
    [SerializeField] private BaseFood[] foodPrefabs;

    private ISnakeController _snakeController;
    private ISnakeFactory _snakeFactory;

    private Dictionary<Type, Stack<BaseFood>> _pool = new();

    [Inject]
    public void Initialize(ISnakeController snakeController, ISnakeFactory snakeFactory)
    {
        _snakeController = snakeController;
        _snakeFactory = snakeFactory;
    }

    public IFood CreateFood(IMapNode node)
    {
        var foodPrefab = foodPrefabs[Random.Range(0, foodPrefabs.Length)];

        switch (foodPrefab)
        {
            case GrowFood:
                var growFood = GetFoodFromPool(typeof(GrowFood), foodPrefab) as GrowFood;
                growFood.Initialize(_snakeFactory, _snakeController, node);
                return growFood;
            case ReduceFood:
                var reduceFood = GetFoodFromPool(typeof(ReduceFood), foodPrefab) as ReduceFood;
                reduceFood.Initialize(_snakeFactory, _snakeController, node);
                return reduceFood;
        }
        
        var baseFood = GetFoodFromPool(typeof(BaseFood), foodPrefab);
        baseFood.Initialize(_snakeController, node);
        return baseFood;
    }

    private BaseFood GetFoodFromPool(Type foodType, BaseFood foodPrefab)
    {
        if (_pool.TryGetValue(foodType, out var foods))
        {
            if (foods.Count > 0)
            {
                return foods.Pop();
            }
        }

        return Instantiate(foodPrefab);
    }

    public void DestroyFood(IDestroyable food)
    {
        switch (food)
        {
            case FlipFood flipFood:
                if (_pool.TryGetValue(typeof(FlipFood), out var flips))
                {
                    flips.Push(flipFood);
                }
                else
                {
                    flips = new Stack<BaseFood>();
                    flips.Push(flipFood);
                    _pool.Add(typeof(FlipFood), flips);
                }
                return;
            case GrowFood growFood:
                if (_pool.TryGetValue(typeof(GrowFood), out var grows))
                {
                    grows.Push(growFood);
                }
                else
                {
                    grows = new Stack<BaseFood>();
                    grows.Push(growFood);
                    _pool.Add(typeof(GrowFood), grows);
                }
                return;
            case ReduceFood reduceFood:
                if (_pool.TryGetValue(typeof(ReduceFood), out var reduces))
                {
                    reduces.Push(reduceFood);
                }
                else
                {
                    reduces = new Stack<BaseFood>();
                    reduces.Push(reduceFood);
                    _pool.Add(typeof(ReduceFood), reduces);
                }
                return;
            case SpeedFood speedFood:
                if (_pool.TryGetValue(typeof(SpeedFood), out var speeds))
                {
                    speeds.Push(speedFood);
                }
                else
                {
                    speeds = new Stack<BaseFood>();
                    speeds.Push(speedFood);
                    _pool.Add(typeof(FlipFood), speeds);
                }
                return;
        }
        
        if (_pool.TryGetValue(typeof(BaseFood), out var bases))
        {
            bases.Push((BaseFood)food);
        }
        else
        {
            bases = new Stack<BaseFood>();
            bases.Push((BaseFood)food);
            _pool.Add(typeof(FlipFood), bases);
        }
    }
}