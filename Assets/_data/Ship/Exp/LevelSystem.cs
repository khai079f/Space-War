using UnityEngine;
using System;

public class LevelSystem : GameMonoBehaviour
{
    [SerializeField] private int MaxLevel = 30;              // Cấp tối đa
    [SerializeField] private int currentLevel = 1;           // Cấp độ hiện tại
    [SerializeField] private float EXPNextLevel = 200f;       // XP cần thiết cho cấp độ tiếp theo
    [SerializeField] private float currentXP = 0f;           // XP hiện tại
    [SerializeField] private float XPxpMultiplier = 1.5f;    // Hệ số nhân XP cho cấp tiếp theo

    // Event khi lên cấp
    public event Action OnLevelUp;
    public void GainXP(float xp)
    {
        this.currentXP += xp;
        SiderExp.Instance.UpdateCurrentValue(this.currentXP);
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (this.currentLevel >= this.MaxLevel || this.currentXP < this.EXPNextLevel) return;

        this.currentXP -= this.EXPNextLevel;
        if (currentXP <= 0) this.currentXP = 0;
        LevelUp();
        this.EXPNextLevel *= this.XPxpMultiplier;
        SiderExp.Instance.UpdateMaxValue(this.EXPNextLevel);
        SiderExp.Instance.UpdateCurrentValue(this.currentXP);
    }

    private void LevelUp()
    {
        currentLevel++;
      //  Debug.Log($"Lên cấp! Cấp độ mới: {currentLevel}");

        // Gọi sự kiện khi lên cấp
        UIEvenLvUp.Instance.Toggle();
        OnLevelUp?.Invoke();
    }
    public virtual float GetEXPNextLevel()
    {
        return this.EXPNextLevel;
    }
    public virtual float GetcurrentXP()
    {
        return this.currentXP;
    }
}
