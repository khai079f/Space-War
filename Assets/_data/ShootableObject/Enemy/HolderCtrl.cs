using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderCtrl : GameMonoBehaviour
{
    protected static HolderCtrl instance;
    public static HolderCtrl Instance { get => instance; }
    [SerializeField] protected List<NormalEnemyCtrl> poolNormalEnemies = new List<NormalEnemyCtrl>(); // Danh sách các enemy hiện có
    [SerializeField] protected List<NormalEnemyCtrl> activeNormalEnemies; // Danh sách các enemy hiện có
    [SerializeField] protected List<NormalEnemyCtrl> usedNormalEnemies; // Danh sách các enemy hiện có

    protected override void Awake()
    {
        base.Awake();
        if (HolderCtrl.instance != null)
        {
            Debug.LogWarning("Only 1 HolderCtrl allowed to exist");
            return; // Dừng lại nếu đã có một instance khác
        }
        HolderCtrl.instance = this;
    }
    private void FixedUpdate()
    {
        this.LoadNormalEnemyCtrl();
    }

    protected virtual void LoadNormalEnemyCtrl()
    {
        // Loại bỏ những enemy trong danh sách nhưng không còn active
        if (this.poolNormalEnemies.Count > 0) this.poolNormalEnemies.RemoveAll(enemy => !enemy.gameObject.activeSelf);
        // Lấy tất cả NormalEnemyCtrl hiện tại là con của FormationController
        NormalEnemyCtrl[] normalEnemies = transform.GetComponentsInChildren<NormalEnemyCtrl>();
        // Thêm các enemy mới chỉ nếu chúng active và chưa có trong danh sách
        foreach (NormalEnemyCtrl enemy in normalEnemies)
        {
            if (enemy.gameObject.activeSelf && !this.poolNormalEnemies.Contains(enemy)) this.poolNormalEnemies.Add(enemy);
        }
        this.UpdateActiveNormalEnemies();
        this.UpdateUsedNormalEnemies();
    }
    protected void UpdateActiveNormalEnemies()
    {
        this.activeNormalEnemies.RemoveAll(enemy => !enemy.gameObject.activeSelf);
        foreach (var enemy in poolNormalEnemies)
        {
            if (!this.activeNormalEnemies.Contains(enemy) && !usedNormalEnemies.Contains(enemy))
            {
                activeNormalEnemies.Add(enemy); // Chỉ thêm enemy vào activeNormalEnemies nếu không có trong usedNormalEnemies
            }
        }
    }


    public NormalEnemyCtrl GetEnemy()
    {

        if (activeNormalEnemies.Count > 0)
        {
            //if (!this.CheckNumberEnemy()) return null;

            NormalEnemyCtrl enemy = activeNormalEnemies[0]; // Lấy enemy đầu tiên
            activeNormalEnemies.RemoveAt(0); // Xóa khỏi activeNormalEnemies
            usedNormalEnemies.Add(enemy); // Thêm vào usedNormalEnemies
            return enemy;
        }
        return null; // Không có enemy nào để lấy
    }
    protected void UpdateUsedNormalEnemies()
    {
        this.usedNormalEnemies.RemoveAll(enemy => !enemy.gameObject.activeSelf);
    }
    public void ReturnEnemy(NormalEnemyCtrl enemy)
    {
        if (usedNormalEnemies.Contains(enemy))
        {
            usedNormalEnemies.Remove(enemy); // Xóa khỏi usedNormalEnemies
            activeNormalEnemies.Add(enemy); // Thêm vào activeNormalEnemies
            enemy.gameObject.SetActive(true); // Kích hoạt enemy trở lại (nếu cần)
        }
    }
    protected virtual bool CheckNumberEnemy()
    {
        if (this.activeNormalEnemies.Count >= 0) return true;
        return false;
    }
}
