using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Food manager
/// </summary>
public class FoodManager : MonoBehaviour
{
    [SerializeField] private BaseFood[] foodPrefabs;
    [SerializeField] private float foodTimer;

    private Map map;
    private GameManager gameManager;
    private BaseFood currentFood;
    private float nextFoodTime;

    private void Start()
    {
        map = FindObjectOfType<Map>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (currentFood == null || Time.time > nextFoodTime)
            GenerateFood();
    }

    private void GenerateFood()
    {
        if (currentFood != null)
        {
            currentFood.FoodEaten -= OnFoodEaten;
            Destroy(currentFood.gameObject);
        }

        if (!map.TryGetFreeNode(out var node))
            return;
        
        var position = new Vector3(node.NodePosition.x, 0, node.NodePosition.y);
        currentFood = Instantiate(PickFood(), position, Quaternion.identity);
        currentFood.FoodEaten += OnFoodEaten;
        nextFoodTime = Time.time + foodTimer;
    }

    private void OnFoodEaten(int score)
    {
        gameManager.AddScore(score);
        currentFood.FoodEaten -= OnFoodEaten;
        Destroy(currentFood.gameObject);
    }

    private BaseFood PickFood()
    {
        return foodPrefabs[Random.Range(0, foodPrefabs.Length)];
    }
}
