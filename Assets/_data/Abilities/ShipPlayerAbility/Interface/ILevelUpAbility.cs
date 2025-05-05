public interface ILevelUpAbility
{
    LevelUpStrategy LevelUpStrategy { get; set; }

    void ResetValue();
}
