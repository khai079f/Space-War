using UnityEngine;
using System;

[Serializable]
public class LevelInfo
{
    public int level;
    public float xpToNextLevel;
    public Action OnLevelUp; // Sự kiện cho cấp độ
}

