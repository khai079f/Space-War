using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipObjDameReceiver : DameReceiver
{
    [Header("Shootable Obj")]
    [SerializeField] protected ShipCtrl shipCtrl;
    [SerializeField] protected float armor = 1;

    protected override void LoadComponents()
    {
        this.LoadShipCtrl();
        base.LoadComponents();

    }

    protected virtual void LoadShipCtrl()
    {
        if (shipCtrl != null) return;
        this.shipCtrl = transform.GetComponentInParent<ShipCtrl>();
        Debug.Log(transform.parent.name + ": LoadShootableObjCtrl", gameObject);
    }

    protected override void OnDead()
    {
        this.OndeadFX();
        this.shipCtrl.Despawn.DeSpawnObj();
        this.hp = 0;
        UIEndGame.Instance.Toggle();

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
        
        this.maxHp = this.shipCtrl.ShipProfileSO.MaxHP;
        this.armor = this.shipCtrl.ShipProfileSO.GetBaseArmor();
        base.Reborn();
    }
    public virtual void IncreaseCurrentArmor(float value)
    {
        this.armor = this.armor + value;
    }
}
