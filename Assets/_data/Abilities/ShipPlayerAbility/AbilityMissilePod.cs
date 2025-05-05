using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMissilePod : AbilityShipAbstract
{
    [SerializeField] protected List<PointShooterMissile> pointShooterWings;
    protected BaseShipCtrl Shooter;
    protected string abilityBullet;
    protected string nameBullet;

    protected int currentShootCount = 0;
    protected int totalShootCount = 0;
    protected float shootInterval = 0.2f;
    protected float shootTimer = 0f;
    protected bool isShooting = false;

    protected override void Awake()
    {
        base.Awake();
        this.SetShooter();
        this.SetNameBullet(BulletSpawner.bulletMissile);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        this.timer += Time.deltaTime;

        if (this.isShooting)
        {
            this.shootTimer += Time.deltaTime;
            if (this.shootTimer >= this.shootInterval)
            {
                this.shootTimer = 0f;
                this.ShootOnce();
                this.currentShootCount++;

                if (this.currentShootCount >= this.totalShootCount)
                {
                    this.isShooting = false;
                }
            }
        }
        else
        {
            this.Shooting();
        }
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
        if (this.pointShooterWings.Count != 0) return;

        foreach (Transform child in transform)
        {
            if (child.name.Contains("WingSupport"))
            {
                PointShooterMissile wing = child.GetComponent<PointShooterMissile>();
                this.pointShooterWings.Add(wing);
            }
        }

        Debug.LogWarning(transform.childCount + ": Loaded WingSupports and disabled them", gameObject);
    }

    protected virtual void Shooting()
    {
        if (!isReady) return;
        if (this.delay > this.timer) return;

        this.totalShootCount = this.abilityData.Lv;
        this.currentShootCount = 0;
        this.shootTimer = 0f;
        this.isShooting = true;

        this.timer = 0f;
        this.Active();
    }

    protected virtual void ShootOnce()
    {
        for (int i = 0; i < pointShooterWings.Count; i++)
        {
            foreach (Transform pointShooterWing in this.pointShooterWings[i].GetPointShooters())
            {
                Transform newBullet = this.GetBullet(this.nameBullet, pointShooterWing.transform);
                if (newBullet == null) continue;

                BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
                bulletCtrl.SetShotter(this.Shooter, this.abilityData);
                newBullet.gameObject.SetActive(true);
                this.pointShooterWings[i].OnSpark();
            }
        }

       // AudioManager.instance.VFXAudioCtrl.GetVFXAudio(VFXAudioType.FXShoot)?.PlayAudioFX();
    }

    protected virtual Transform GetBullet(string bulletName, Transform pointShooter)
    {
        Vector3 spawnPos = pointShooter.position;

        float angle = pointShooter.eulerAngles.z;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        return BulletSpawner.Instance.Spawn(bulletName, spawnPos, rotation);
    }

    protected virtual void SetShooter()
    {
        this.Shooter = this.shipPlayAbleAbilities.ShipCtrl;
    }

    protected virtual string GetNameAbility()
    {
        return this.abilityBullet = "MissilePod";
    }

    protected virtual void SetNameBullet(string nameBullet)
    {
        this.nameBullet = nameBullet;
    }
}
