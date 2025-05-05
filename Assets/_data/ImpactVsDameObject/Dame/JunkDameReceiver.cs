using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkDameReceiver : DameReceiver
{
    [Header("Junk")]
    [SerializeField] protected JunkCtrl junkCtrl;

    protected override void LoadComponents()
    {
        this.LoadJunkCtrl();
        base.LoadComponents();

    }

    protected virtual void LoadJunkCtrl()
    {
        if (junkCtrl != null) return;
        this.junkCtrl = transform.GetComponentInParent<JunkCtrl>();
        Debug.Log(transform.name + ": LoadJunkCtrl", gameObject);
    }

    protected override void OnDead()
    {
        this.OndeadFX();
        this.OnDeadDrop();
        this.junkCtrl.Despawn.DeSpawnObj();
       
    }

    protected virtual void OnDeadDrop()
    {
        Vector3 dropPos = transform.position;
        Quaternion dropRot = transform.rotation;
        ItemDropSpawner.Instance.Drop(this.junkCtrl.ShootableObject.DropList, dropPos,dropRot);
    }
    protected virtual void OndeadFX()
    {
        string fxName = this.GetOnDeadFXName();
        Transform fxOnDead = FXSpawner.Instance.Spawn(fxName, transform.position, transform.rotation);
        fxOnDead.gameObject.SetActive(true);

    }

    protected virtual string GetOnDeadFXName()
    {
        return FXSpawner.smokeOne;
    }

    protected override void Reborn()
    {
        this.maxHp = this.junkCtrl.ShootableObject.GetHp(); 
        base.Reborn();
    }
}
