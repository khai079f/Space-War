using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDash : AbilityShipAbstract
{
    [Header("Ability Warp")]
    protected Vector4 keyDirection;
    [SerializeField] protected bool isDashing = false;
    [SerializeField] protected float dashDistance = 3f;
    [SerializeField] protected Vector4 dashDirection;
    [SerializeField] protected float manaUse = 0;
    protected Vector3 dashTargetPos;
    protected float dashSpeed = 10f;
    protected float dashTimer = 0f;
    protected float dashDuration = 0.3f;


    protected override void Start()
    {
        base.Start();
        this.GetManaUse();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        this.CheckWarpDirection();

    }
    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (isDashing)
        {
            this.UpdateDash();
        }
        else
        {
            this.Dashing(); // bắt đầu dash nếu đủ điều kiện
        }
    }

    protected virtual void CheckWarpDirection()
    {
        if (!this.isReady) return;
        if (this.isDashing) return;
        if (this.keyDirection.x == 1) this.DashLeft();
        if (this.keyDirection.y == 1) this.DashRight();
        if (this.keyDirection.z == 1) this.DashUp();
        if (this.keyDirection.w == 1) this.DashDown();
    }

    protected virtual void DashLeft()
    {
       // Debug.Log("WarpLeft");
        this.dashDirection.x = 1;
    }

    protected virtual void DashRight()
    {
      //  Debug.Log("WarpRight");
        this.dashDirection.y = 1;
    }

    protected virtual void DashUp()
    {
       // Debug.Log("WarpUp");
        this.dashDirection.z = 1;
    }

    protected virtual void DashDown()
    {
      //  Debug.Log("WarpDown");
        this.dashDirection.w = 1;
    }

    protected virtual void Dashing()
    {
        if (this.isDashing) return;
        if (this.IsDirectionNotSet()) return;
        if (!this.ShipPlayAbleAbilities.DeductMana(this.manaUse)) return;
        this.SetDashTarget();
        this.isDashing = true;
        this.dashTimer = 0f; // reset timer mỗi lần dash
    }

    protected virtual void SetDashTarget()
    {
        Transform obj = this.shipPlayAbleAbilities.ShipCtrl.transform;
        dashTargetPos = obj.position;

        if (dashDirection.x == 1) dashTargetPos.x -= dashDistance;
        if (dashDirection.y == 1) dashTargetPos.x += dashDistance;
        if (dashDirection.z == 1) dashTargetPos.y += dashDistance;
        if (dashDirection.w == 1) dashTargetPos.y -= dashDistance;
    }

protected virtual void UpdateDash()
{
    Transform obj = this.shipPlayAbleAbilities.ShipCtrl.transform;
    Quaternion initialRotation = obj.rotation;

    // Tăng thời gian dash
    dashTimer += Time.fixedDeltaTime;

    // Spawn dash effect mỗi frame
    Transform fx = FXSpawner.Instance.Spawn(FXSpawner.dashOne, obj.position, initialRotation);
    fx.gameObject.SetActive(true);

    // Di chuyển
    obj.position = Vector3.MoveTowards(obj.position, dashTargetPos, dashSpeed * Time.fixedDeltaTime);
    obj.rotation = initialRotation;

    // Nếu vượt quá 0.3 giây thì kết thúc dash
    if (dashTimer >= dashDuration)
    {
        //obj.position = dashTargetPos; // Snap đến vị trí cuối
        this.dashDirection.Set(0, 0, 0, 0);
        this.isDashing = false;
        this.Active();
    }
}

    public override void Active()
    {
        base.Active();
    }
    protected virtual bool IsDirectionNotSet()
    {
        return this.dashDirection == Vector4.zero;
    }

    protected virtual void GetManaUse()
    {
        this.manaUse = this.abilityData.ManaUse;
    }

    public override void UseAbilitySkill()
    {
        Debug.Log("SetAbilitySkill");
    }
}
