using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityNormalShoot : BaseAbility
{
    [SerializeField] protected List<PointShooterManager> pointShooters;
    protected BaseShipCtrl Shooter;
    protected string abilityBullet;
    protected string nameBullet;

    protected override void Awake()
    {
        base.Awake();
        this.SetShooter();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPointShooter();
    }
    protected override void LoadAbilityData()
    {
        if (this.abilityData != null) return;
        string resPath = "ShootableObject/Enemy/Ability/"+ this.GetNameAbility();
        this.abilityData = Resources.Load<AbilityData>(resPath);
        Debug.LogWarning(transform.name + ": LoadAbilityData for Enemy", gameObject);
    }
    protected virtual void LoadPointShooter()
    {
        // Đệ quy lấy tất cả các con
        if (this.pointShooters.Count != 0) return;
        foreach (Transform child in transform)
        {
            if (child.name.Contains("PointShooter")) this.pointShooters.Add(child.GetComponent<PointShooterManager>()) ;
        }

        Debug.LogWarning(transform.childCount + ": Loaded PointShooters", gameObject);
    }
    public virtual void Shoot()
    {
        if (!isReady) return;
        if (this.delay > this.timer) return;
        foreach (PointShooterManager pointShooter in this.pointShooters)
        {
            Transform newBullet = this.GetBullet(this.GetNameBullet(), pointShooter.transform);
            if (newBullet == null) return;
            BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
            bulletCtrl.SetShotter(this.Shooter, this.abilityData);
            newBullet.gameObject.SetActive(true);
            pointShooter.OnSpark();
        }
        this.Active();

        ///Debug.Log("Shooting");
    }
    protected virtual Transform GetBullet(string bulletName,Transform pointShooter)
    {
        Vector3 spawnPos = pointShooter.position;
        Quaternion rotation = pointShooter.rotation;
        return BulletSpawner.Instance.Spawn(bulletName, spawnPos, rotation); ;
    }
    protected abstract void SetShooter();

    protected virtual string GetNameAbility()
    {
        return this.abilityBullet = "NormalShoot";
    }
    protected virtual string GetNameBullet()
    {
        return this.nameBullet = BulletSpawner.bulletTwo;

    }

}

