using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LazerPlayerShootImpact : AbilitiesShipImpact
{
    [Header("Lazer Impact")]
    [SerializeField] protected AbilityShootLazerBoost abilityShootLazer;
    [SerializeField] protected float damageInterval = 1f / 60f; // Khoảng thời gian gây sát thương mỗi 1/60 giây
    [SerializeField] protected BoxCollider2D boxCollider;
    private float damageTimer;

    protected override void LoadCollider()
    {
        base.LoadCollider();
        if (this.colliderShip == null) return;
        this.boxCollider = (BoxCollider2D)this.colliderShip;
    }
    protected override void LoadCtrlForShooter()
    {
        base.LoadCtrlForShooter();
        if (this.abilityShip == null) return;
        this.abilityShootLazer = (AbilityShootLazerBoost)this.abilityShip;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.ShouldIgnoreCollisionWithSelf(other)) return;
        if (this.CheckTypeShooter(other)) return;
        if (!this.abilityShootLazer.IsShootingLazerBoost) return;
        this.abilityShootLazer.LazerDameSender.SendDameLazer(other.transform, this.abilityShootLazer.DameBoost, 0);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (this.ShouldIgnoreCollisionWithSelf(other)) return;
        if (this.CheckTypeShooter(other)) return;
        if (this.abilityShootLazer.IsShootingLazerBoost) return;
        // Cập nhật bộ đếm thời gian
        damageTimer += Time.deltaTime;

        // Nếu thời gian đạt đến khoảng cách cần thiết (1/60 giây), gây sát thương
        if (damageTimer >= damageInterval )
        {
            if (other == null) return;
            // sử dụng nội tại
            this.abilityShootLazer.Intrinsic.SetInTriggerState(true);
          //  Debug.Log("dame by time: "+ this.abilityShootLazer.DameBoost);
            this.abilityShootLazer.LazerDameSender.SendDameLazer(other.transform, this.abilityShootLazer.DameBoost, 60); // Gây sát thương
            damageTimer = 0f; // Reset lại bộ đếm thời gian
        }
        if (this.abilityShootLazer.DameBoost >= 1 && !this.abilityShootLazer.isShootingBoost)
        {
            if (other == null) return;
            this.abilityShootLazer.LazerDameSender.SendDameLazer(other.transform, this.abilityShootLazer.DameBoost, 0); // Gây sát thương
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        // sử dụng nội tại
        this.abilityShootLazer.Intrinsic.SetInTriggerState(false);
        // Reset bộ đếm khi đối tượng rời khỏi vùng laser
        damageTimer = 0f;
    }

    public void UpdateColliderSize()
    {
        if (abilityShootLazer == null || this.abilityShootLazer.ModelLazerCtrl.LineRenderer == null) return;

        Vector3 startPoint = this.abilityShootLazer.ModelLazerCtrl.LineRenderer.GetPosition(0);
        Vector3 endPoint = this.abilityShootLazer.ModelLazerCtrl.LineRenderer.GetPosition(1);
        float laserLength = Vector3.Distance(startPoint, endPoint);
        float laserWidth = (this.abilityShootLazer.ModelLazerCtrl.LineRenderer.startWidth + this.abilityShootLazer.ModelLazerCtrl.LineRenderer.endWidth) / 2;

        boxCollider.size = new Vector2(laserLength, laserWidth);
        boxCollider.offset = new Vector2(laserLength / 2, boxCollider.offset.y);
        boxCollider.transform.position = startPoint;
    }


    private bool ShouldIgnoreCollisionWithSelf(Collider2D other) => other.transform.parent == transform.parent.parent;
}

