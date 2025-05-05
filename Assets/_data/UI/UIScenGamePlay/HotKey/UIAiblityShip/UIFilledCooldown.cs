using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFilledCooldown : UIAbilityHotKeyAbstract 
{
    [SerializeField] protected Image imageFilled;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIImageFilled();
    }
    protected virtual void LoadUIImageFilled()
    {
        if (imageFilled != null) return;
        this.imageFilled = transform.GetComponent<Image>();
         Debug.LogWarning(transform.name + " LoadUIImageFilled ", gameObject);
    }
    private void FixedUpdate()
    {
        this.UpdateFilledImage();
    }
    protected virtual void UpdateFilledImage()
    {
        if (this.baseAbility == null) return;

        // Lấy cooldown hiện tại và cooldown tối đa
        float cooldownCurrent = this.baseAbility.GetTimer();
        float cooldownMax = this.baseAbility.AbilityData.Cooldown;

        // Nếu kỹ năng đã sẵn sàng (cooldownCurrent <= 0), ẩn hình ảnh hoặc đặt fillAmount về 0
        if (cooldownCurrent <= 0)
        {
            this.imageFilled.fillAmount = 0;
            return;
        }

        // Tính toán tỉ lệ cooldown
        float fillAmount = 1 - (cooldownCurrent / cooldownMax);

        // Cập nhật giá trị fillAmount
        this.imageFilled.fillAmount = fillAmount;
    }

}
