using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Food manager
/// </summary>
public class FoodManager : MonoBehaviour
{
    private float _foodTimer;
    private IMap _map;
    private IGameManager _gameManager;
    private IFoodFactory _foodFactory;
    private IFood _currentFood;
    private float _nextFoodTime;

    public void Initialize(IMap map, IGameManager gameManager, IFoodFactory foodFactory, IAppSettingsService appSettingsService)
    {
        _foodTimer = appSettingsService.GameSettings.FoodTimer;
        _map = map;
        _gameManager = gameManager;
        _foodFactory = foodFactory;
    }

    private void Update()
    {
        if (_currentFood == null || Time.time > _nextFoodTime)
            GenerateFood();
    }

    private void GenerateFood()
    {
        if (_currentFood != null)
        {
            ClearFood();
        }

        if (!_map.TryGetFreeNode(out var node))
            return;

        _currentFood = _foodFactory.CreateFood(node);
        _currentFood.FoodEaten += OnFoodEaten;

        _nextFoodTime = Time.time + _foodTimer;
    }

    private void OnFoodEaten(int score)
    {
        _gameManager.AddScore(score);
        ClearFood();
    }

    private void ClearFood()
    {
        _currentFood.FoodEaten -= OnFoodEaten;
        var food = _currentFood as BaseFood;
        Destroy(food.gameObject);
        _currentFood = null;
    }
}
