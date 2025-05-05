using System.Collections.Generic;
using UnityEngine;

public class IntrinsicPrecisionBeam : AbilityShipAbstract
{
    [Header("Precision Beam Settings")]
    [SerializeField] private AbilityShootLazerBoost abilityLazerBoost;
    [SerializeField] private bool isInTrigger = false; // Trạng thái trong trigger
    [SerializeField] private int maxStacks = 5; // Số lần cộng dồn tối đa
    [SerializeField] private float damageInterval = 0.5f; // Khoảng cách thời gian giữa các lần tăng sát thương
    [SerializeField] private float damageDecayInterval = 1f; // Thời gian giữa mỗi lần giảm sát thương
    [SerializeField] private float damageIncreaseRate; // Tỉ lệ tăng sát thương (10%)

    private int currentStacks = 0; // Số lần cộng dồn hiện tại
    private List<float> damageIncreases = new List<float>(); // Danh sách lưu trữ các giá trị tăng sát thương
    private bool previousIsInTrigger; // Biến lưu trạng thái trước đó của isInTrigger

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        // Gọi hàm xử lý tăng hoặc giảm sát thương
        HandleDamageChange();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityLazerCtrl();
    }
    protected override void ResetValue()
    {
        base.ResetValue();
        this.damageIncreaseRate = this.abilityData.Dame/10;
    }
protected virtual void LoadAbilityLazerCtrl()
{
    if (this.abilityLazerBoost != null) return;
    if (this.shipPlayAbleAbilities == null) return;

    // Kiểm tra danh sách BaseShipAbilitys và tìm AbilityLazerCtrl
    foreach (var ability in this.shipPlayAbleAbilities.BaseShipAbilitys)
    {
        if (ability is AbilityShootLazerBoost lazerCtrl)
        {
            this.abilityLazerBoost = lazerCtrl;
            break; // Dừng ngay khi tìm thấy
        }
    }
}
    // Hàm xử lý tăng và giảm sát thương
    private void HandleDamageChange()
    {
        // Kiểm tra nếu trạng thái thay đổi
        if (isInTrigger != previousIsInTrigger)
        {
            timer = 0f; // Reset bộ đếm thời gian
            previousIsInTrigger = isInTrigger; // Cập nhật trạng thái trước đó
        }

        if (isInTrigger) // Nếu trong trigger, tăng sát thương
        {
            timer += Time.fixedDeltaTime; // Tăng bộ đếm thời gian
            if (timer >= damageInterval && currentStacks < maxStacks)
            {
                IncreaseBaseDamage(); // Tăng sát thương
                currentStacks++; // Tăng số lần cộng dồn
                timer = 0f; // Reset thời gian
            }
        }
        else if (currentStacks > 0 && this.abilityLazerBoost != null) // Nếu không trong trigger, giảm sát thương
        {
            if (this.abilityLazerBoost.IsBoosting) return;
            timer += Time.fixedDeltaTime; // Tăng bộ đếm thời gian
            if (timer >= damageDecayInterval)
            {
                DecreaseBaseDamage(); // Giảm sát thương
                currentStacks--; // Giảm số lần cộng dồn
                timer = 0f; // Reset thời gian
            }
        }
    }

    private void IncreaseBaseDamage()
    {
        float damageIncrease = shipPlayAbleAbilities.ShipCtrl.ShipProfileSO.BaseDame * damageIncreaseRate;
        shipPlayAbleAbilities.ShipCtrl.ShipProfileSO.UpdateBaseDame(damageIncrease);

        // Lưu lại giá trị tăng sát thương
        damageIncreases.Add(damageIncrease);

  //      Debug.Log($"Base Damage Increased: {shipPlayAbleAbilities.ShipCtrl.ShipProfileSO.BaseDame} (Stacks: {currentStacks + 1})");
    }

    private void DecreaseBaseDamage()
    {
        if (damageIncreases.Count > 0)
        {
            // Lấy giá trị tăng sát thương cuối cùng từ danh sách và giảm lại
            float damageDecrease = damageIncreases[damageIncreases.Count - 1];
            shipPlayAbleAbilities.ShipCtrl.ShipProfileSO.UpdateBaseDame(-damageDecrease);

            // Xóa giá trị đã giảm
            damageIncreases.RemoveAt(damageIncreases.Count - 1);

       //     Debug.Log($"Base Damage Decreased: {shipPlayAbleAbilities.ShipCtrl.ShipProfileSO.BaseDame} (Stacks: {currentStacks})");
        }
    }

    // Phương thức để thay đổi trạng thái trigger
    public void SetInTriggerState(bool state)
    {
        isInTrigger = state;
    }
    public override int GetValueStackForUI()
    {
        return this.damageIncreases.Count;
    }

}
