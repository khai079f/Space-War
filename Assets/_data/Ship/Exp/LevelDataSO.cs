using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "SO/ShipLevelData", order = 1)]
public class LevelDataSO : ScriptableObject
{
    public List<LevelInfo> levels;
    public int maxLevels = 100;       // Số cấp tối đa
    public float baseXP = 200f;       // XP cho cấp đầu tiên
    public float xpMultiplier = 1.5f; // Hệ số nhân XP cho mỗi cấp mới

    [ContextMenu("Generate Levels")]
    public void GenerateLevels()
    {
        levels = new List<LevelInfo>();

        for (int i = 1; i <= maxLevels; i++)
        {
            float xpToNext = baseXP * Mathf.Pow(xpMultiplier, i - 1);
            levels.Add(new LevelInfo { level = i, xpToNextLevel = xpToNext });
        }
    }
}
