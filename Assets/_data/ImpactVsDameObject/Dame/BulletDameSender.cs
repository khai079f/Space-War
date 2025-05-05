using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDameSender : DameSender
{
    [SerializeField] protected BulletCtrl bulletCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
    }
    protected virtual void LoadBulletCtrl()
    {
        if (bulletCtrl != null) return;
        this.bulletCtrl = transform.GetComponentInParent<BulletCtrl>();
        Debug.Log(transform.name + ": LoadBulletCtrl", gameObject);
    }

    protected virtual void SetValueForBullet()
    {
        this.dame = this.bulletCtrl.AbilityData.Dame;
    }
    public override void Send(DameReceiver dameReceiver)
    {
        this.DestroyBullet();
        this.SetValueForBullet();
        base.Send(dameReceiver);
        this.CreateImpactFX();
    }

    protected virtual void DestroyBullet()
    {
        this.bulletCtrl.Despawn.DeSpawnObj();
    }
    protected virtual void CreateImpactFX()
    {
        if (FXSpawner.Instance == null) return;
        Transform impactFX = FXSpawner.Instance.Spawn(FXSpawner.impactOne, transform.position, transform.rotation);
        impactFX.gameObject.SetActive(true);
    }
}
