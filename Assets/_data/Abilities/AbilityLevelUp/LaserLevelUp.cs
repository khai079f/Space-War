using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLevelUp : LevelUpStrategy
{
    protected AbilityLazerCtrl AbilityLazerCtrl;

    // Danh sách màu sắc HDR cho mỗi cấp độ của laser với cường độ sáng cao, có pha thêm màu trắng
    private List<Color> levelColors = new List<Color>
    {
        new Color(0f, 0f, 1f) * 3f,    // Xanh da trời với cường độ 3
        new Color(0f, 0f, 0.5f) * 3f,  // Xanh dương với cường độ 3
        new Color(1f, 1f, 0f) * 3f,    // Vàng với cường độ 3
        new Color(1f, 0.5f, 0f) * 2.5f,  // Cam với cường độ 2.5
        new Color(1f, 0f, 0f) * 2f     // Đỏ với cường độ 2
    };

    public LaserLevelUp(AbilityLazerCtrl abilityLazerCtrl) : base(abilityLazerCtrl)
    {
        this.AbilityLazerCtrl = abilityLazerCtrl;
    }

    public override void LevelUp(AbilityData abilityData)
    {
/*        // Tăng cấp và độ dày của laser
        abilityData.IncreaseStatus(abilityData.contentLevelup.damageIncrease, abilityData.contentLevelup.cooldownReduction, abilityData.contentLevelup.manaUseReduction);
        AbilityLazerCtrl.UpdateLaserWidth(AbilityLazerCtrl.LineRenderer.startWidth + 0.1f);

        // Chọn màu tương ứng với cấp độ hiện tại của laser
        int level = Mathf.Clamp(abilityData.Lv - 1, 0, levelColors.Count - 1);
        Color newColor = levelColors[level];

        // Pha trộn thêm một phần màu trắng vào màu gốc (tùy theo cấp độ)
        float whiteMixFactor = Mathf.Min(0.5f, (abilityData.Lv - 1) * 0.1f); // Tỉ lệ pha trộn trắng tăng dần theo cấp độ
        Color finalColor = Color.Lerp(newColor, Color.white, whiteMixFactor);

        // Cập nhật màu laser sử dụng phương thức UpdateLaserColorHDR
        AbilityLazerCtrl.UpdateLaserColorHDR(finalColor.r, finalColor.g, finalColor.b);*/
    }
}
