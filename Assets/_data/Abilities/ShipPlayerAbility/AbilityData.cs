using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityData", menuName = "Ability/AbilityData")]
public class AbilityData : ScriptableObject
{
    public TypeObject typeObject = TypeObject.nonType;
    public string nameAbility;
    [Header("Base Status")]
    [SerializeField] private float baseDame = 1f;
    [SerializeField] private float baseCooldown = 2f;
    [SerializeField] private float baseManaUse = 2f;
    [SerializeField] private int baseLv = 1;
    [Header("Current Status")]
    [SerializeField] private float dame = 1f;
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private float manaUse = 2f;
    [SerializeField] private int lv = 1;
    [SerializeField] private string description = "";
    public Sprite sprite;
    public bool isActiveSkill = true;

    public float Dame => dame;
    public float Cooldown => cooldown;
    public float ManaUse => manaUse;
    public int Lv => lv;
    public string Description => description;

    [Header("Level Up Content")]

    public ContentLevelup contentLevelup;

    public event Action OnLevelUp;
    public void Leveling(LevelUpStrategy levelUpStrategy)
    {
        levelUpStrategy.LevelUp(this);
        this.lv++;
        OnLevelUp?.Invoke();
    }
    public virtual void IncreaseStatus(float dame , float cooldown, float manaUse)
    {
        this.dame += dame;

        // Đảm bảo cooldown không nhỏ hơn 0
        this.cooldown = Mathf.Max(0, this.cooldown - cooldown);

        // Đảm bảo manaUse không nhỏ hơn 0
        this.manaUse = Mathf.Max(0, this.manaUse - manaUse);
    }

    public void SetBaseValue()
    {
       // Debug.Log("SetBaseValue");
        this.lv = this.baseLv;
        this.dame = this.baseDame;
        this.cooldown = Mathf.Max(0, this.baseCooldown);  // Đảm bảo cooldown không âm
        this.manaUse = Mathf.Max(0, this.baseManaUse);    // Đảm bảo manaUse không âm
    }
}
