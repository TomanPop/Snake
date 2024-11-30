public interface IFoodFactory
{
    public IFood CreateFood(IMapNode node);
    public void DestroyFood(IDestroyable food);
}