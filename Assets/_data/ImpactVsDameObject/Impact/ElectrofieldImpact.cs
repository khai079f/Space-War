using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrofieldImpact : AbilitiesShipImpact
{
    [Header("ElectrofieldImpact")]
    [SerializeField] protected CircleCollider2D circleCollider;
    protected AbilityElectrofield abilityElectrofield;

    protected override void LoadCtrlForShooter()
    {
        base.LoadCtrlForShooter();
        if (this.abilityShip == null) return;
        this.abilityElectrofield = (AbilityElectrofield)this.abilityShip;
    }

    protected override void LoadCollider()
    {
        base.LoadCollider();
        if (this.colliderShip == null) return;
        this.circleCollider = (CircleCollider2D)this.colliderShip;
    }

    private void LateUpdate()
    {
        if (this.abilityElectrofield == null) return;
        float newRadius = this.abilityElectrofield.LargestParticleSize / 2 - 0.6f;
        this.UpdateSizeCircleCollider(newRadius);
    }

    protected virtual void UpdateSizeCircleCollider(float newSize)
    {
        if (this.circleCollider == null) return;
        this.circleCollider.radius = newSize;
    }

    // Khi có va chạm bắt đầu
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.ShouldIgnoreCollisionWithSelf(other)) return;
        if (this.CheckTypeShooter(other)) return;
        this.abilityShip.DameSender.Send(other.transform);

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (this.ShouldIgnoreCollisionWithSelf(other)) return;
        if (this.CheckTypeShooter(other)) return;
        this.abilityElectrofield.DameByTime(other);
        
    }

    private bool ShouldIgnoreCollisionWithSelf(Collider2D other) => other.transform.parent == transform.parent.parent;
}
