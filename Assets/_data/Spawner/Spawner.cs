using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : GameMonoBehaviour
{
    [SerializeField] protected Transform holder;
    public  Transform Holder => holder;
    [SerializeField] protected List<Transform> prefabs;
    public List<Transform> Prefabs => prefabs;

    [SerializeField] protected List<Transform> poolObjs;
    [SerializeField] protected int spawnedCount = 0;
    public int SpawnedCount => spawnedCount;
    public event Action<Transform> OnDespawn;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
        this.LoadHolder();
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        Debug.Log(transform.name + ": LoadHolder", gameObject);

    }
    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) this.prefabs.Clear();
        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }
        this.HidePrefab();

        //Debug.Log(transform.name + ": LoadPrefabs", gameObject);
    }

    protected virtual void HidePrefab()
    {
        foreach (Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }
    protected virtual Transform GetObjectFromPool(Transform prefabName)
    {
        foreach (Transform poolobj in poolObjs)
        {
            if (prefabName.name == poolobj.name)
            {
                this.poolObjs.Remove(poolobj);
                return poolobj;
            }
        }
        Transform newPrefab = Instantiate(prefabName);
        newPrefab.name = prefabName.name;
        return newPrefab;
    }
    public virtual Transform Spawn(string prefabName, Vector3 spawnPos, Quaternion rotation)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.LogWarning(transform.name+":prefab Not found " + prefabName,gameObject);
            return null;
        }
        Transform newPrefab = this.GetObjectFromPool(prefab);
        newPrefab.SetPositionAndRotation(spawnPos, rotation);

        newPrefab.SetParent(this.holder);
        this.spawnedCount++;
        return newPrefab;

    }
    public virtual void Despawn(Transform obj)
    {
        if (this.poolObjs.Contains(obj)) return;
        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
        this.spawnedCount--;
        OnDespawn?.Invoke(obj);
    }
    public virtual Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == prefabName) return prefab;

        }
        return null;
    }
    public virtual void BackHold(Transform obj)
    {
        obj.parent = this.holder;
    }
    public virtual void ActiveObj(Transform obj, bool isActive)
    {
        obj.gameObject.SetActive(isActive);

    }
}
