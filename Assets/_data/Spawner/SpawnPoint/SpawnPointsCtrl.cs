using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsCtrl : GameMonoBehaviour
{
    [SerializeField] protected List<Transform> spawnPoints;
    public List<Transform> SpawnPoints { get => spawnPoints; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnPoints();
    }
    protected virtual void LoadSpawnPoints()
    {
        if (this.spawnPoints == null)
        {
            this.spawnPoints = new List<Transform>();
        }
        else if (this.spawnPoints.Count > 0)
        {
            this.spawnPoints.Clear();
        }

        foreach (Transform spawnPoint in transform)
        {
            this.spawnPoints.Add(spawnPoint);
        }
        //Debug.Log(transform.name + ": LoadSpawnPoints", gameObject);
    }
    public virtual Transform GetRandom()
    {
        if (this.spawnPoints == null || this.spawnPoints.Count == 0)
        {
            Debug.LogWarning(transform.name + ": No spawn points available.", gameObject);
            return null;
        }

        int randomIndex = Random.Range(0, this.spawnPoints.Count);
        return this.spawnPoints[randomIndex];
    }
}
