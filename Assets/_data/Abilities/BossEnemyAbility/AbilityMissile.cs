using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMissile : AbilityNormalShoot
{
    [Header("Ability Missile")]
    [SerializeField] protected AbilityMissileMananger manangerMissile;
    [SerializeField] private int bulletsPerShot = 3; // Số lượng đạn mỗi lần bắn
    [SerializeField] private float angleOffset = 10f; // Góc lệch giữa các viên đạn

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityMissileMananger();
    }
    protected virtual void LoadAbilityMissileMananger()
    {
        if (this.manangerMissile != null) return;
        this.manangerMissile = transform.GetComponentInParent<AbilityMissileMananger>();
        Debug.LogWarning(transform.name + " LoadAbilityMissileMananger", gameObject);
    }
    protected override string GetNameAbility()
    {
        return this.abilityBullet = "MissileShoot";
    }

    protected override string GetNameBullet()
    {
        return this.nameBullet = BulletSpawner.bulletMissile;
    }

    public override void Shoot()
    {
        if (!isReady) return;
        if (this.delay > this.timer) return;

        this.timer = 0;

        foreach (PointShooterManager pointShooter in this.pointShooters)
        {
            float baseAngle = pointShooter.transform.eulerAngles.z;
            float startAngle = baseAngle - (angleOffset * (bulletsPerShot - 1) / 2);

            for (int i = 0; i < bulletsPerShot; i++)
            {
                float currentAngle = startAngle + (angleOffset * i);
                Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);
                Transform newBullet = this.GetBullet(this.GetNameBullet(), pointShooter.transform.position, rotation);

                if (newBullet == null) return;

                BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
                bulletCtrl.SetShotter(this.Shooter, this.abilityData);
                newBullet.gameObject.SetActive(true);
            }

            pointShooter.OnSpark();
            this.Active();
        }
    }
    protected override void SetShooter()
    {
        if (this.manangerMissile == null) return;
        this.Shooter = this.manangerMissile.abilitySilverlight.BaseEnemyCtrl;

    }
    protected Transform GetBullet(string bulletName, Vector3 position, Quaternion rotation)
    {
        return BulletSpawner.Instance.Spawn(bulletName, position, rotation);
    }
}
