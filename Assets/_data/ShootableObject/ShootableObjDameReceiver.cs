using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObjDameReceiver : DameReceiver
{
    [Header("Shootable Obj")]
    [SerializeField] protected BaseEnemyCtrl baseEnemyCtrl;

    protected override void LoadComponents()
    {
        this.LoadShootableObjCtrl();
        base.LoadComponents();

    }

    protected virtual void LoadShootableObjCtrl()
    {
        // Kiểm tra nếu đã load shootableObjCtrl
        if (baseEnemyCtrl != null) return;

        // Tìm kiếm đối tượng cha bất kể kiểu generic cụ thể
        var baseEnemy = transform.GetComponentInParent<BaseEnemyCtrl>();

        // Ép kiểu an toàn
        if (baseEnemy != null && baseEnemy is BaseEnemyCtrl enemy)
        {
            this.baseEnemyCtrl = enemy;
            Debug.Log(transform.parent.name + ": Loaded shootableObjCtrl as BaseEnemyCtrl", gameObject);
        }
        else
        {
            Debug.LogError(transform.parent.name + ": Không tìm thấy BaseEnemyCtrl<AbilityEnemyCtrlAbstract> phù hợp", gameObject);
        }
    }

    protected override void OnDead()
    {
        this.OndeadFX();
        this.OnDeadDrop();
        this.baseEnemyCtrl.Despawn.DeSpawnObj();

    }

    protected virtual void OnDeadDrop()
    {
        Vector3 dropPos = transform.position;
        Quaternion dropRot = transform.rotation;
        ItemDropSpawner.Instance.Drop(this.baseEnemyCtrl.EnemySO.DropList, dropPos, dropRot);
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
        this.maxHp = this.baseEnemyCtrl.EnemySO.GetHp();
        base.Reborn();
    }
}
