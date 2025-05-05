using System.Collections.Generic;
using UnityEngine;

public class SpawnByLevel : SpawnerCtrl
{
    [SerializeField] private List<ObjectSpawnerByLevel> objectSpawnerByLevels = new List<ObjectSpawnerByLevel>();
    [SerializeField] protected float spawnDelay = 1f;
    private float spawnTimer = 0f;

    protected override void Start()
    {
        base.Start();
        foreach(ObjectSpawnerByLevel objectSpawner in this.objectSpawnerByLevels)
        {
            objectSpawner.SetCurentLimitNumberSpawn();
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.InitializeSpawnerByLevels();
    }

    protected virtual void FixedUpdate()
    {
        this.EnemySpawning();
    }

    protected virtual void EnemySpawning()
    {
        this.spawnTimer += Time.fixedDeltaTime;
        if (this.spawnTimer < this.spawnDelay) return;
        this.spawnTimer = 0;

        Transform point = GetRandomSpawnPoints(this.SpawnPoints.SpawnPoints);
        if (point == null) return;

        Transform objPrefab = this.GetRandomPrefab();
        if (objPrefab == null) return;

        Vector3 pos = point.position;
        Quaternion rot = point.rotation;
        Transform enemy = spawner.Spawn(objPrefab.name, pos, rot);
        if (enemy == null) return;
        enemy.gameObject.SetActive(true); // Activate the object
        //enemy.InitializeStateForEnemy(new RetreatState(enemy, enemy.StateEnemy.stateMachine, enemy.AbilityCtrl.LookAtEnemy.GetPlayerPos()));
    }

    protected virtual void InitializeSpawnerByLevels()
    {
        if (this.spawner.Prefabs == null || this.spawner.Prefabs.Count == 0) return;
        if (this.spawner.Prefabs.Count == objectSpawnerByLevels.Count) return;
        foreach (Transform prefab in this.spawner.Prefabs)
        {
            ObjectSpawnerByLevel spawnerByLevel = new ObjectSpawnerByLevel();
            spawnerByLevel.SetObjectSpawner(prefab);
            spawnerByLevel.SetNumberSpawnByLevel(1);
            objectSpawnerByLevels.Add(spawnerByLevel);
        }
    }

    protected virtual Transform GetRandomSpawnPoints(List<Transform> spawnPoints)
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning("No SpawnPoints available");
            return null;
        }

        int randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex];
    }

    // Lấy một prefab ngẫu nhiên có thể spawn
    protected virtual Transform GetRandomPrefab()
    {
        List<ObjectSpawnerByLevel> validSpawners = this.objectSpawnerByLevels.FindAll(spawner => spawner.CanSpawn());

        if (validSpawners.Count == 0) return null;

        int randomIndex = Random.Range(0, validSpawners.Count);
        ObjectSpawnerByLevel selectedSpawner = validSpawners[randomIndex];

        // Tăng số lượng đã spawn
        selectedSpawner.IncrementSpawnedCount();

        return selectedSpawner.GetObjectSpawner();
    }
    protected virtual void IncrementNumberSpawn()
    {
        foreach(ObjectSpawnerByLevel objectSpawner in this.objectSpawnerByLevels)
        {
            objectSpawner.IncrementNumberSpawnByLevel();
        }
    }
    private void DecreaseNumberSpawn(Transform despawnedObject)
    {
        foreach (ObjectSpawnerByLevel spawner in objectSpawnerByLevels)
        {

            // Kiểm tra nếu tên của đối tượng trùng với spawner
            if (spawner.GetObjectSpawner().name == despawnedObject.name && despawnedObject.parent.name == "Holder")
            {
                spawner.DecreaseSpawnedCount();
                return; // Thoát khỏi vòng lặp sau khi giảm
            }
        }

       // Debug.LogWarning($"No matching spawner found for despawned object: {despawnedObject.name}");
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        if (MapLevelByTime.Instance != null)
        {
            MapLevelByTime.Instance.OnLevelUp += IncrementNumberSpawn;
        }
        if (this.spawner != null) this.spawner.OnDespawn += DecreaseNumberSpawn;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if (MapLevelByTime.Instance != null)
        {
            MapLevelByTime.Instance.OnLevelUp -= IncrementNumberSpawn;
        }
        if (this.spawner != null) this.spawner.OnDespawn -= DecreaseNumberSpawn;
    }
}
