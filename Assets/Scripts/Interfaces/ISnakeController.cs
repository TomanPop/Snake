public interface ISnakeController
{
    public void Move(MoveDirection moveDirection);
    public IBody GetSnakeBody();
    public void SetSnakeBody(IBody body);
    public void UnregisterBuff(SpeedBuff speedBuff);
    public void RegisterBuff(SpeedBuff speedBuff);
}