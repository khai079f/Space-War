using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityWingSupport : AbilityShipAbstract
{
    [SerializeField] protected List<PointShooterWing> pointShooterWings;
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
        if (this.pointShooterWings.Count != 0) return;

        foreach (Transform child in transform)
        {
            if (child.name.Contains("WingSupport"))
            {
                PointShooterWing wing = child.GetComponent<PointShooterWing>();
                this.pointShooterWings.Add(wing);

                // Set inactive khi mới load
                wing.gameObject.SetActive(false);
            }
        }

        Debug.LogWarning(transform.childCount + ": Loaded WingSupports and disabled them", gameObject);
    }


    protected virtual void Shooting()
    {
        if (!isReady) return;
        if (this.delay > this.timer) return;

        // Số lượng PointShooter được bắn dựa trên cấp độ
        this.UnlockShootByLv();
        for (int i = 0; i < pointShooterWings.Count; i++)
        {
            foreach(Transform pointShooterWing in this.pointShooterWings[i].GetPointShooters())
            {
                Transform newBullet = this.GetBullet(this.nameBullet, pointShooterWing.transform);
                if (newBullet == null) return;

                BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
                bulletCtrl.SetShotter(this.Shooter, this.abilityData);
                newBullet.gameObject.SetActive(true);
                this.pointShooterWings[i].OnSpark();
            }
        }
        this.Active();
    }
    protected virtual void UnlockShootByLv()
    {
        int lv = this.abilityData.Lv;
        for (int i = 0; i < this.pointShooterWings.Count; i++)
        {
            if (lv < 1)
            {
                this.pointShooterWings[i].gameObject.SetActive(false);
            }
            if (lv == 1)
            {
                this.pointShooterWings[0].gameObject.SetActive(true);
                this.pointShooterWings[0].UpdateStatuspointShooters(true, 1);
                if (this.pointShooterWings.Count > 1) this.pointShooterWings[1].gameObject.SetActive(false);
                break; // không cần tiếp tục vòng lặp
            }
            if (lv == 2)
            {
                this.pointShooterWings[i].gameObject.SetActive(true);
                this.pointShooterWings[i].UpdateStatuspointShooters(true, 1);
            }
            if (lv == 3)
            {
                this.pointShooterWings[0].UpdateStatuspointShooters(true, 2);
            }
            if (lv == 4)
            {
                this.pointShooterWings[i].UpdateStatuspointShooters(true, 2);
            }
            if (lv == 5)
            {
                this.SetNameBullet(BulletSpawner.bulletThree);
            }
        }

        return ;
    }

    protected virtual Transform GetBullet(string bulletName, Transform pointShooter)
    {
        Vector3 spawnPos = pointShooter.position;

        // Tính toán góc quay viên đạn với góc lệch được truyền vào
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
        return this.abilityBullet = "AbilityWingSupport";
    }
    protected virtual void SetNameBullet(string nameBullet)
    {
        this.nameBullet = nameBullet;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        abilityData.OnLevelUp += UnlockShootByLv;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        abilityData.OnLevelUp -= UnlockShootByLv;
    }
}
