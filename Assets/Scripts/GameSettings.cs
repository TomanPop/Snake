using System;

[Serializable]
public class GameSettings
{
    public int MapSizeX = 10;
    public int MapSizeY = 10;
    public float SnakeBaseTimeStep = 0.5f;
    public float FoodTimer = 10f;
}