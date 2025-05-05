using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnByLevel : SpawnByLevel
{
    protected override void OnEnable()
    {
        if (MapLevelByTime.Instance != null)
        {
            MapLevelByTime.Instance.OnLevelUp += IncrementNumberSpawn;
        }
    }
    protected override void OnDisable()
    {
        if (MapLevelByTime.Instance != null)
        {
            MapLevelByTime.Instance.OnLevelUp -= IncrementNumberSpawn;
        }
    }
}
