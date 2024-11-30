using System;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public int highScore;
    public int lastScore;
    public MoveDirection lastMoveDirection;
    public Vector2Int[] lastBodyParts;
}