using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class ObjectSpawnerByLevel
{
    [SerializeField] private Transform objectSpawner; // Prefab
    [SerializeField] protected int numberSpawnByLevel; 
    [SerializeField] protected int curentLimitNumberSpawn; // Số lượng tối đa có thể spawn
    [SerializeField] private int currentSpawnedCount = 0; // Số lượng đã spawn hiện tại
    [SerializeField] protected LevelSpawnSetting levelSpawnSetting = new LevelSpawnSetting();
    public void SetCurentLimitNumberSpawn()
    {
        if (this.levelSpawnSetting.spawnType == LevelSpawnType.EveryLevel ) this.curentLimitNumberSpawn = this.numberSpawnByLevel;
        this.levelSpawnSetting.UpdateCustomLevelsFromType();

    }
    public Transform GetObjectSpawner() => objectSpawner;
    public void SetObjectSpawner(Transform spawner) => objectSpawner = spawner;

    public float GetNumberSpawnByLevel() => numberSpawnByLevel;
    public void SetNumberSpawnByLevel(int value) => numberSpawnByLevel = value;
    public void IncrementNumberSpawnByLevel()
    {
        if (this.levelSpawnSetting.spawnType == LevelSpawnType.EveryLevel)
        {
            this.curentLimitNumberSpawn = this.numberSpawnByLevel * MapLevelByTime.Instance.LevelCurrent;
            return;
        }
        this.levelSpawnSetting.UpdateCustomLevelsFromType();
        if (this.levelSpawnSetting.customLevels == null) return;
        if (this.levelSpawnSetting.GetCustomLevelList().Contains(MapLevelByTime.Instance.LevelCurrent)) this.curentLimitNumberSpawn += this.numberSpawnByLevel;

    }
    public int GetCurrentSpawnedCount() => currentSpawnedCount;
    public void IncrementSpawnedCount() => currentSpawnedCount++;
    public void DecreaseSpawnedCount() => currentSpawnedCount--;
    public bool CanSpawn() => currentSpawnedCount < curentLimitNumberSpawn;
}
