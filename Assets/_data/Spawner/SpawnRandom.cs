using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandom : GameMonoBehaviour
{
    [Header("Spawn random")]

    [SerializeField] protected SpawnerCtrl spawnCtrl;
    [SerializeField] protected float randomDelay = 1f;
    [SerializeField] protected float randomTimer = 0f;
    [SerializeField] protected int randomLimit ;

    protected virtual void FixedUpdate()
    {
        this.JunkSpawning();
    }

    // Start spawning objects at intervals
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadJunkSpawnCtrl();
    }

    protected virtual void LoadJunkSpawnCtrl()
    {
        if (this.spawnCtrl != null) return;
        this.spawnCtrl = GetComponent<SpawnerCtrl>();
        Debug.LogWarning(transform.name + ": LoadSpawnPoints ", gameObject);
    }
    protected virtual void JunkSpawning()
    {
        if (this.RandomReachLimit()) return;
        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        this.randomTimer = 0;

        Transform point = GetRandomSpawnPoints(this.spawnCtrl.SpawnPoints.SpawnPoints);
        if (point == null) return; // Thêm kiểm tra null
        Vector3 pos = point.position;
        Quaternion rot = point.rotation;

        Transform obj = spawnCtrl.Spawner.Spawn(this.GetRandomPrefab(), pos,rot);

        //Transform obj = JunkSpawner.Instance.Spawn(JunkSpawner.meteoriteOne, pos, rot); // Spawn object
        if (obj != null)
        {
            obj.gameObject.SetActive(true); // Activate the object
        }
    }

    protected virtual Transform GetRandomSpawnPoints(List<Transform> spawnPoints)
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning("No SpawnPoints available");
            return null;
        }

        // Lấy chỉ số ngẫu nhiên từ danh sách spawnPoints
        int randomIndex = Random.Range(0, spawnPoints.Count);

        // Trả về phần tử ngẫu nhiên từ danh sách
        return spawnPoints[randomIndex];
    }
    protected virtual string GetRandomPrefab()
    {
        if (this.spawnCtrl.Spawner.Prefabs == null || this.spawnCtrl.Spawner.Prefabs.Count == 0)
        {
            Debug.LogWarning("No PrefabJunk available");
            return null;
        }

        // Lấy chỉ số ngẫu nhiên từ danh sách spawnPoints
        int randomIndex = Random.Range(0, this.spawnCtrl.Spawner.Prefabs.Count);
        //Debug.Log(this.spawnCtrl.Spawn.Prefabs[randomIndex].name);
        // Trả về phần tử ngẫu nhiên từ danh sách
        return this.spawnCtrl.Spawner.Prefabs[randomIndex].name;
    }
    protected virtual bool RandomReachLimit()
    {
        int currentJunk = this.spawnCtrl.Spawner.SpawnedCount;
        return currentJunk == randomLimit;
    }
}
