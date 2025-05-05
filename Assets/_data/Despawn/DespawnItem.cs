using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnItem : DespawnByTime
{
    public override void DeSpawnObj()
    {
        ItemDropSpawner.Instance.Despawn(transform.parent);
    }
    protected override void ResetValue()
    {
        this.delay = 10f;

    }
}
