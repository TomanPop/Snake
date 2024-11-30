using UnityEngine;

public static class GameContext
{
    public static int BeginScore { get; set; }
    public static MoveDirection BeginDirection { get; set; }
    public static Vector2Int[] SnakePositions { get; set; }
}