public class LevelUpAbilityHelper : ILevelUpAbility
{
    public LevelUpStrategy LevelUpStrategy { get; set; }
    private AbilityData abilityData;

    public LevelUpAbilityHelper(AbilityData abilityData,BaseAbility baseAbility)
    {
        this.abilityData = abilityData;
        WhatLevelUpStrategy(baseAbility);
        RegisterEvents();
    }

    public void WhatLevelUpStrategy(BaseAbility baseAbility)
    {
        LevelUpStrategy = new LevelUpStrategy(baseAbility);
    }

    public void ResetValue()
    {
        // Logic reset giá trị khi level up
    }

    private void RegisterEvents()
    {
        if (abilityData != null)
            abilityData.OnLevelUp += ResetValue;
    }

    public void UnregisterEvents()
    {
        if (abilityData != null)
            abilityData.OnLevelUp -= ResetValue;
    }
}
