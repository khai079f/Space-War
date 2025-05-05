using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCtrl : GameMonoBehaviour
{
    [Header("Spawner Ctrl")]
    [SerializeField] protected Spawner spawner;
    public Spawner Spawner => spawner; 

    [SerializeField] protected SpawnPointsCtrl spawnPoints;
    public SpawnPointsCtrl SpawnPoints => spawnPoints;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnPoints();
        this.LoadSpawn();
    }

    protected virtual void LoadSpawnPoints()
    {
        if (this.spawnPoints != null) return;
        this.spawnPoints =GameObject.Find("ScenePoints").transform.Find("SpawnerPoints").GetComponent<SpawnPointsCtrl>();
        if (this.spawnPoints == null) Debug.LogWarning("JunkSpawnPoints object not found in the scene.");
        Debug.Log(transform.name + ": LoadSpawnPoints ", gameObject);

    }

    protected virtual void LoadSpawn()
    {
        if (this.spawner != null) return;
        this.spawner = transform.GetComponent<Spawner>();
        if (this.spawner == null) Debug.LogWarning("JunkSpawn object not found in the scene.");
        Debug.Log(spawner.name + ": LoadJunkSpawn ", gameObject);

    }
}
