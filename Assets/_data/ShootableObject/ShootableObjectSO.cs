using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootableObject", menuName = "SO/ShootableObject")]
public class ShootableObjectSO : ScriptableObject
{
    [SerializeField] private string objName = "Shootable Object";
    [SerializeField] private ObjectType objType;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float baseDame = 1;
    [SerializeField] private float baseMaxHp = 2;
    [SerializeField] private float baseMaxMana = 2;
    [SerializeField] private float baseManaRecovery = 0.3f;
    [SerializeField] private float baseSpeed = 5;
    [SerializeField] private List<DropRate> dropList;
    // Getter
    public string ObjName => objName;
    public ObjectType ObjType => objType;
    public Sprite Sprite => sprite;
    public List<DropRate> DropList => dropList;
    public float GetHp()
    {
        if (MapLevelByTime.Instance == null) return baseMaxHp;
        return baseMaxHp + (baseMaxHp * (MapLevelByTime.Instance.LevelCurrent / 5));
    }
    public float GetDamage()
    {
        if (MapLevelByTime.Instance == null) return baseDame;
        return baseDame + (baseDame * (MapLevelByTime.Instance.LevelCurrent / 5f));
    }

    public float GetMana()
    {
        if (MapLevelByTime.Instance == null) return baseMaxMana;
        return baseMaxMana + (baseMaxMana * (MapLevelByTime.Instance.LevelCurrent / 5f));
    }

    public float GetRecoveryMana()
    {
        if (MapLevelByTime.Instance == null) return baseManaRecovery;
        return baseManaRecovery + (baseManaRecovery * (MapLevelByTime.Instance.LevelCurrent / 5f));
    }

    public float GetSpeed()
    {
        if (MapLevelByTime.Instance == null) return baseSpeed;
        return baseSpeed + (baseSpeed * (MapLevelByTime.Instance.LevelCurrent / 5f));
    }
}

