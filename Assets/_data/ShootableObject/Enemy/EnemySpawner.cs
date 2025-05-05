using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [Header("Enemy Spawner")]
    // Singleton pattern to ensure only one instance of JunkSpawner exists
    protected static EnemySpawner instance;
    public static EnemySpawner Instance => instance;
    // Delegate và event cho Observer Pattern
    protected override void Awake()
    {
        base.Awake();
        if (EnemySpawner.instance != null)
        {
            Debug.LogWarning("Only 1 EnemySpawner allowed to exist");
            return; // Dừng lại nếu đã có một instance khác
        }
        EnemySpawner.instance = this;
    }
    public virtual BaseEnemyCtrl SpawnEnemy(string prefabName, Vector3 spawnPos, Quaternion rotation)
    {
        BaseEnemyCtrl prefab = this.GetPrefabByName(prefabName).GetComponent<BaseEnemyCtrl>();
        if (prefab == null)
        {
            Debug.LogWarning(transform.name + ":prefab Not found " + prefabName, gameObject);
            return null;
        }
        BaseEnemyCtrl newPrefab = this.GetEnemyFromPool(prefab);
        newPrefab.transform.SetPositionAndRotation(spawnPos, rotation);

        newPrefab.transform.SetParent(this.holder);
        this.spawnedCount++;
        return newPrefab;

    }
    protected virtual BaseEnemyCtrl GetEnemyFromPool(BaseEnemyCtrl prefabName)
    {
        // Tìm đối tượng trong pool có cùng tên
        Transform poolObj = poolObjs.Find(obj => obj.name == prefabName.name);
        if (poolObj != null)
        {
            poolObjs.Remove(poolObj);
            return poolObj.GetComponent<BaseEnemyCtrl>();
        }

        // Tạo đối tượng mới nếu không tìm thấy
        BaseEnemyCtrl newPrefab = Instantiate(prefabName);
        newPrefab.name = prefabName.name;
        return newPrefab;
    }
    public virtual void WaitForAppearFinishAndAddHPBar(Transform newEnemy)
    {
        NormalEnemyCtrl newEnemyCtrl = newEnemy.GetComponent<NormalEnemyCtrl>();

        // Đợi đến khi quá trình xuất hiện hoàn thành
        // Khi đã xuất hiện hoàn tất, thêm thanh HP
        this.AddHPBarToObj(newEnemy);
    }

    protected virtual void AddHPBarToObj(Transform newEnemy)
    {
        BaseEnemyCtrl newEnemyCtrl = newEnemy.GetComponent<BaseEnemyCtrl>();
        Transform newHPBar = HPSpawner.Instance.Spawn(HPSpawner.HPBar, newEnemy.position, Quaternion.identity);
        HPBar hpBar = newHPBar.GetComponent<HPBar>();
        hpBar.SetObjCtrl(newEnemyCtrl);
        hpBar.SetFollowTarget(newEnemy);
        newHPBar.gameObject.SetActive(true);
    }

/*    protected virtual bool IsAppearFinish(NormalEnemyCtrl newEnemyCtrl)
    {
        // Kiểm tra trạng thái xuất hiện của đối tượng
        return newEnemyCtrl.AbilityNormalEnemyCtrl.AppearBigger.gameObject.activeSelf;
    }*/
}