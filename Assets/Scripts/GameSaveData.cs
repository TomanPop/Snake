using System;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public int score;
    public MoveDirection moveDirection;
    public Vector2Int[] bodyParts;
}