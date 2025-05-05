using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ContentLevelup
{
    [Tooltip("Amount of damage to add per level up")]
    public float damageIncrease = 0.5f;

    [Tooltip("Amount of cooldown reduction per level up")]
    public float cooldownReduction = 0.1f;

    [Tooltip("Amount of mana reduction per level up")]
    public float manaUseReduction = 0.2f;

    [Tooltip("Description of stats increase per level")]
    public string description = "Increases damage, reduces cooldown and mana use per level";
}
