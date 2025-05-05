using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootableObjabstract: GameMonoBehaviour
{
    [SerializeField] protected BaseEnemyCtrl baseEnemyCtrl;
    public BaseEnemyCtrl BaseEnemyCtrl => baseEnemyCtrl;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShootableObjCtrl();

    }

    protected virtual void LoadShootableObjCtrl()
    {
        if (this.baseEnemyCtrl != null) return;
        this.baseEnemyCtrl = transform.GetComponentInParent<BaseEnemyCtrl>();
        Debug.LogWarning(transform.name + ": LoadShootableObjCtrl", gameObject);
    }

}
