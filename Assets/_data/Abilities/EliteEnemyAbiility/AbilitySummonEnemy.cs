using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AbilitySummonEnemy : AbilitySummon
{
    [SerializeField] protected List<Transform> minions;
    [SerializeField] protected int minionLimit = 5;


    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        this.clearDeadMinions();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();

    }
    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        GameObject enmeySpawner = GameObject.Find("EnemySpawner");
        this.spawner = enmeySpawner.GetComponent<EnemySpawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }
    protected override void Summoning()
    {
        if (this.minions.Count >= this.minionLimit) return;
        base.Summoning();
    }

/*    protected override Transform Summon()
    {
        Transform minion = base.Summon();
        minion.parent = this.transform;
        this.minions.Add(minion);
        return minion;
    }*/

    protected virtual void clearDeadMinions()
    {
        foreach(Transform minion in this.minions)
        {
            if(minion.gameObject.activeSelf == false)
            {
                this.minions.Remove(minion);
                return;
            }
        }
    }
}
