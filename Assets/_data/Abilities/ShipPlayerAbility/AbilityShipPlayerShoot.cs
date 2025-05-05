using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShipPlayerShoot : AbilityShipAbstract
{
    [SerializeField] protected List<PointShooterManager> pointShooters;
    [SerializeField] protected float angleOffset = 10f; // Độ lệch góc cơ bản
    protected bool isShootAngleOffset =false;
    protected BaseShipCtrl Shooter;
    protected string abilityBullet;
    protected string nameBullet;

    protected override void Awake()
    {
        base.Awake();
        this.SetShooter();
        this.SetNameBullet(BulletSpawner.bulletTwo);
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        this.Shooting();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPointShooter();
    }
    protected override void LoadAbilityData()
    {
        if (this.abilityData != null) return;
        string resPath = "ShootableObject/Ship/Ability/" + this.GetNameAbility();
        this.abilityData = Resources.Load<AbilityData>(resPath);
        Debug.LogWarning(transform.name + ": LoadAbilityData for Enemy", gameObject);
    }
    protected virtual void LoadPointShooter()
    {
        if (this.pointShooters.Count != 0) return;
        foreach (Transform child in transform)
        {
            if (child.name.Contains("PointShooter")) this.pointShooters.Add(child.GetComponent<PointShooterManager>());
        }

        Debug.LogWarning(transform.childCount + ": Loaded PointShooters", gameObject);
    }

    public virtual void Shooting()
    {
        if (!isReady) return;
        if (this.delay > this.timer) return;

        // Số lượng PointShooter được bắn dựa trên cấp độ
        int shootersToFire = this.UnlockShootByLv();

        for (int i = 0; i < shootersToFire; i++)
        {
            PointShooterManager pointShooter = this.pointShooters[i];

            // Tính toán góc lệch dựa trên thứ tự chẵn/lẻ
            float adjustedAngleOffset = GetAngleOffset(i);
            Transform newBullet = this.GetBullet(this.nameBullet, pointShooter.transform, adjustedAngleOffset);
            if (newBullet == null) return;

            BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
            bulletCtrl.SetShotter(this.Shooter, this.abilityData);
            newBullet.gameObject.SetActive(true);
            pointShooter.OnSpark();
        }
        AudioManager.instance.VFXAudioCtrl.GetVFXAudio(VFXAudioType.FXShoot)?.PlayAudioFX();
        this.Active();
    }

    protected virtual Transform GetBullet(string bulletName, Transform pointShooter, float adjustedAngleOffset)
    {
        Vector3 spawnPos = pointShooter.position;

        // Tính toán góc quay viên đạn với góc lệch được truyền vào
        float angle = pointShooter.eulerAngles.z + adjustedAngleOffset;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        return BulletSpawner.Instance.Spawn(bulletName, spawnPos, rotation);
    }
    protected virtual float GetAngleOffset(int lv)
    {
        if (!this.isShootAngleOffset) return 0;
        if (lv == 0) return 0;
        if (lv == 1) return -this.angleOffset;
        if (lv == 2) return this.angleOffset;
        if (lv == 3) return -(this.angleOffset +this.angleOffset);
        if (lv == 4) return this.angleOffset + this.angleOffset;
        return 0;
    }
    public virtual void SetStateShootAngleOffset(bool state)
    {
         this.isShootAngleOffset= state;
    }
    public virtual bool GetStateShootAngleOffset()
    {
        return this.isShootAngleOffset;
    }
    protected virtual void SetShooter()
    {
        this.Shooter = this.shipPlayAbleAbilities.ShipCtrl;
    }
    protected virtual string GetNameAbility()
    {
        return this.abilityBullet = "FireShooting";
    }
    protected virtual void SetNameBullet(string nameBullet)
    {
         this.nameBullet = nameBullet ;
    }
    protected virtual int UnlockShootByLv()
    {
        return Mathf.Min(this.abilityData.Lv, this.pointShooters.Count);
    }
}
