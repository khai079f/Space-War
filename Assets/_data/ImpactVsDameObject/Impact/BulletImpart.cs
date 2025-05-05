using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BulletImpart : ImpactAbstract
{
    [Header("Bullet Impart")]
    [SerializeField] protected BulletCtrl bulletCtrl;
    [SerializeField] protected CircleCollider2D circleCollider;
    protected override void LoadCollider()
    {
        if (this.circleCollider != null) return;
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.circleCollider.isTrigger = true;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }
    protected override void LoadCtrlForShooter()
    {
        if (bulletCtrl != null) return;
        this.bulletCtrl = transform.GetComponentInParent<BulletCtrl>();
        Debug.Log(transform.name + ": LoadBulletCtrl", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.CheckShooter(other)) return;
        if (this.CheckTypeShooter(other)) return;
        this.bulletCtrl.DameSender.Send(other.transform);

    }
    protected virtual bool CheckShooter(Collider2D other)
    {
        Transform currentTransform = other.transform;
        // Duyệt qua tất cả các cấp độ parent cho đến khi đạt đến root
        if (currentTransform.parent.transform == this.bulletCtrl.Shooter.transform)
        {
            return true;
        }
        return false;
    }
    protected override string GetTypeShooter()
    {
        if (this.bulletCtrl.Shooter == null) return null;
        this.baseShooter = this.bulletCtrl.Shooter;
        return this.shooterType=this.baseShooter.WhatType();
    }

}

