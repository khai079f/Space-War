using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawnCtrl : GameMonoBehaviour
{
    [SerializeField] protected JunkSpawner junkSpawn;
    public JunkSpawner JunkSpawn { get => junkSpawn; }

    [SerializeField] protected SpawnPointsCtrl spawnPoints;
    public SpawnPointsCtrl SpawnPoints { get => spawnPoints; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnPoints();
        this.LoadJunkSpawn();
    }

    protected virtual void LoadSpawnPoints()
    {
        if (this.spawnPoints != null) return;
        this.spawnPoints = FindObjectOfType<SpawnPointsCtrl>();
        if (this.spawnPoints == null) Debug.LogWarning("JunkSpawnPoints object not found in the scene.");
        Debug.Log(transform.name+ ": LoadSpawnPoints ",gameObject);

    }

    protected virtual void LoadJunkSpawn()
    {
        if (this.junkSpawn != null) return;
        this.junkSpawn = transform.GetComponentInParent<JunkSpawner>();
        if (this.junkSpawn == null) Debug.LogWarning("JunkSpawn object not found in the scene.");
        Debug.Log(junkSpawn.name + ": LoadJunkSpawn ", gameObject);

    }
}
