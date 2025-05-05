using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityLevelUp : BaseAbility
{
    public LevelUpStrategy levelUpStrategy;
    protected override void OnEnable()
    {
        // Đăng ký hàm HandleLevelUp với event OnLevelUp
        abilityData.OnLevelUp += ResetValue;
    }

    protected override void OnDisable()
    {
        // Hủy đăng ký hàm HandleLevelUp khi đối tượng bị vô hiệu hóa
        abilityData.OnLevelUp -= ResetValue;
    }
    protected override void Start()
    {
        base.Start();
        this.WhatlevelUpStrategy();
    }
    public virtual void WhatlevelUpStrategy()
    {

        this.levelUpStrategy = new LevelUpStrategy(this);
    }
}
