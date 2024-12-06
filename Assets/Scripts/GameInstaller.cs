using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Map map;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIController uiController;
    [SerializeField] private SnakeController snakeController;
    [SerializeField] private SnakeFactory snakeFactory;
    [SerializeField] private FoodFactory foodFactory;
    
    public override void InstallBindings()
    {
        Container.Bind<IMap>().FromInstance(map);
        Container.Bind<ICommandInvoker>().To<CommandInvoker>().AsSingle();
        Container.Bind<ISnakeController>().FromInstance(snakeController);
        Container.Bind<IGameManager>().FromInstance(gameManager);
        Container.Bind<IUIController>().FromInstance(uiController);
        Container.Bind<IFoodFactory>().FromInstance(foodFactory);
        Container.Bind<ISnakeFactory>().FromInstance(snakeFactory);
    }
}
