using System.Collections;
using UnityEngine;

public class AbilityShootLazer : AbilityLazerCtrl
{
    [Header("Lazer Settings")]
    [SerializeField] private float poinEndLazer = 0f;
    [SerializeField] protected float dameBoost = 0f;
    public float DameBoost => dameBoost;
    public bool isShootingBoost;

    protected override void OnUpdate()
    {
        base.OnUpdate();
        this.HandleLazerInput();
    }
    protected virtual void HandleLazerInput()
    {

        StartLazer();
        UpdateLazer();
        if (Input.GetMouseButton(0))
        {
            this.shipPlayAbleAbilities.DeductMana(this.abilityData.ManaUse, 0.5f);
            this.dameBoost = this.abilityData.Dame / 1.5f;
            this.isShootingBoost = true;
            this.modelLazerCtrl.UpdateStartVFX(150, new Color(0f, 0.5f, 1f, 1f), 1.5f); // Màu xanh dương, kích thước lớn (1.5 - 2.0)
        }
        if (Input.GetMouseButtonUp(0))
        {
            this.dameBoost = 0f;
            this.isShootingBoost = false;
            this.modelLazerCtrl.UpdateStartVFX(20, new Color(1f, 0.5f, 0f, 1f), 0.5f); // Màu cam, kích thước nhỏ (0.5 - 1.5)
        }
    }

    protected void StartLazer()
    {
        // Bật laser với hiệu ứng đầy đủ (cả start và end)
        this.ModelLazerCtrl.SetLazerState(this.lazerImpact, true, true, true);
    }

    protected void StopLazer()
    {
        // Tắt laser nhưng vẫn giữ start point
        this.ModelLazerCtrl.SetLazerState(this.lazerImpact, true, false, false);
    }

    private void UpdateLazer()
    {
        // Đặt vị trí ban đầu của laser
        this.ModelLazerCtrl.LineRenderer.SetPosition(0, shootPoint.position);

        // Tính hướng laser
        Vector2 direction = shootPoint.up * -1;
        this.ModelLazerCtrl.StartVFX.transform.position = shootPoint.position;

        // Kiểm tra va chạm bằng Raycast
        RaycastHit2D closestHit = FindClosestHit(Physics2D.RaycastAll(shootPoint.position, direction, Mathf.Infinity));

        if (closestHit.collider != null)
        {
           // Debug.Log($"Hit object: {closestHit.collider.name}");
            this.ModelLazerCtrl.LineRenderer.SetPosition(1, closestHit.point);
        }
        else
        {
            Vector2 cameraEdgePoint = (Vector2)InputManager.Instance.GetCameraEdgePoint(shootPoint.position, direction)
                                      + direction.normalized * poinEndLazer;
            this.ModelLazerCtrl.LineRenderer.SetPosition(1, cameraEdgePoint);
        }

        // Cập nhật vị trí hiệu ứng cuối laser và va chạm
        this.ModelLazerCtrl.EndVFX.transform.position = this.ModelLazerCtrl.LineRenderer.GetPosition(1);
        lazerImpact.UpdateColliderSize();
    }


    private RaycastHit2D FindClosestHit(RaycastHit2D[] hits)
    {
        RaycastHit2D closestHit = default;
        float closestDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (hit.collider == null) continue;

            // Bỏ qua nếu là chính mình hoặc cùng cha
            if (hit.collider.transform.root == this.transform.root) continue;

            // Bỏ qua nếu không có component nhận dame
            if (!HasDameReceiver(hit)) continue;

            float distance = Vector2.Distance(shootPoint.position, hit.point);
            if (distance < closestDistance)
            {
                closestHit = hit;
                closestDistance = distance;
            }
        }

        return closestHit;
    }


    private bool ShouldProcessHit(RaycastHit2D hit)
    {
        return hit.collider != null
               && !ShouldIgnoreCollisionWithSelf(hit.collider)
               && HasDameReceiver(hit);
    }

    public override void UseAbilitySkill()
    {
        StartLazer(); // Kích hoạt bắn laser khi sử dụng kỹ năng
    }
}
