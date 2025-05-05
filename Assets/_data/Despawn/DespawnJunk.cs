using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnJunk : DespawnByDistance
{

    public override void DeSpawnObj()
    {
        JunkSpawner.Instance.Despawn(transform.parent);
    }

    protected override void ResetValue()
    {
        this.disLimit = 20f;

    }
}