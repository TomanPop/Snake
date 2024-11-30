public interface ISnakeFactory
{
    public IBody CreateSnakeBodyPart(IMapNode node);
    public void DestroySnakeBodyPart(IDestroyable bodyPart);
}