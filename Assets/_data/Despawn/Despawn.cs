using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : GameMonoBehaviour
{
    protected virtual void FixedUpdate()
    {
        this.Despawning();
    }

    protected virtual void Despawning()
    {
        if (!CanDespawn()) return;
        this.DeSpawnObj();
    }

    public virtual void DeSpawnObj()
    {
        // for override
        this.WhatFX();
    }
    public virtual void DeSpawnObj(BaseShipCtrl baseShipCtrl,AbilityData abilityData){
        // for override
    }
    public virtual void DeSpawnObj(BaseShipCtrl baseShipCtrl)
    {
        // for override
        this.WhatFX();
    }

    protected abstract bool CanDespawn();
    protected abstract void WhatFX();
}
