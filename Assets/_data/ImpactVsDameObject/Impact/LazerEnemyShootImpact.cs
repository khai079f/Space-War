using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LazerEnemyShootImpact : ImpactAbstract
{
    [Header("Lazer Impact")]
    [SerializeField] protected AbilityEnemyAttackLazer abilityShootLazer;
    [SerializeField] protected float damageInterval = 1f / 60f; // Khoảng thời gian gây sát thương mỗi 1/60 giây
    [SerializeField] protected BoxCollider2D boxCollider;
    private float damageTimer;
    protected override string GetTypeShooter()
    {
        if (this.abilityShootLazer == null) return null;
        this.baseShooter = this.abilityShootLazer.AbilityEnemyCtrl.BaseEnemyCtrl;
        if (this.baseShooter == null) return null;
        return this.shooterType = this.baseShooter.WhatType();
    }

    protected override void LoadCollider()
    {
        if (this.boxCollider != null) return;
        this.boxCollider = GetComponent<BoxCollider2D>();
        this.boxCollider.isTrigger = true;
        Debug.LogWarning(transform.name + ":LoadCollider", gameObject);
    }

    protected override void LoadCtrlForShooter()
    {
        if (abilityShootLazer != null) return;
        this.abilityShootLazer = GetComponentInParent<AbilityEnemyAttackLazer>();
        Debug.LogWarning(transform.name + ":LoadCtrlForShooter", gameObject);
    }
    public void UpdateColliderSize()
    {
        if (this.abilityShootLazer == null || this.abilityShootLazer.ModelLazerCtrl.LineRenderer == null) return;

        Vector3 startPoint = this.abilityShootLazer.ModelLazerCtrl.LineRenderer.GetPosition(0);
        Vector3 endPoint = this.abilityShootLazer.ModelLazerCtrl.LineRenderer.GetPosition(1);
        float laserLength = Vector3.Distance(startPoint, endPoint);
        float laserWidth = (this.abilityShootLazer.ModelLazerCtrl.LineRenderer.startWidth + abilityShootLazer.ModelLazerCtrl.LineRenderer.endWidth) / 2;

        this.boxCollider.size = new Vector2(laserLength, laserWidth);
        this.boxCollider.offset = new Vector2(laserLength / 2, this.boxCollider.offset.y);
        this.boxCollider.transform.position = startPoint;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.ShouldIgnoreCollisionWithSelf(collision)) return;
        if (this.CheckTypeShooter(collision)) return;
        this.abilityShootLazer.LazerDameSender.Send(collision.transform); // Gây sát thương

    }
    private bool ShouldIgnoreCollisionWithSelf(Collider2D other) => other.transform.parent == transform.parent.parent;
}
