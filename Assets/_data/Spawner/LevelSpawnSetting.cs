using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum LevelSpawnType
{
    [InspectorName("Every Level")]
    EveryLevel,

    [InspectorName("Elite Levels (3,5,7...30)")]
    EliteLevels1,
    [InspectorName("Elite Levels (5,10,15...30)")]
    EliteLevels2,
    [InspectorName("Custom Levels")]
    CustomLevels
}

[System.Serializable]
public class LevelSpawnSetting
{
    public LevelSpawnType spawnType = LevelSpawnType.EveryLevel;
    [HintText("Ví dụ: 5,10,15")]
    public string customLevels;
    public List<int> GetCustomLevelList()
    {
        if (string.IsNullOrWhiteSpace(customLevels)) return new List<int>();

        return customLevels
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => {
                int.TryParse(s.Trim(), out int val);
                return val;
            })
            .Where(x => x > 0)
            .ToList();
    }
    public void UpdateCustomLevelsFromType()
    {
        switch (spawnType)
        {
            case LevelSpawnType.EliteLevels1:
                customLevels = string.Join(",", Enumerable.Range(3, 28).Where(i => i % 2 == 1));
                break;

            case LevelSpawnType.EliteLevels2:
                customLevels = string.Join(",", Enumerable.Range(1, 30).Where(i => i % 5 == 0));
                break;

            case LevelSpawnType.CustomLevels:
            case LevelSpawnType.EveryLevel:
            default:
                // Không gán gì cả
                break;
        }
    }
}
