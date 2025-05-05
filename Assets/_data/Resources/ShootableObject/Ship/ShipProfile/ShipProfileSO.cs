using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipProfileSO", menuName = "SO/ShipProfile")]
public class ShipProfileSO : ScriptableObject
{
    [Header("original status")]
    [SerializeField] private ShipCode shipCode = ShipCode.NoShip;
    [SerializeField] private ShipName shipName = ShipName.NoShip;
    [SerializeField] private TypeObject typeObject = TypeObject.ShipPlayer;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float originalDame = 1f;
    [SerializeField] private float originalMaxHP = 1000;
    [SerializeField] private float originalArmor = 5f;
    [SerializeField] private float originalMana = 10f;
    [SerializeField] private float originalManaRecovery = 1f;
    [Header("base status")]
    [SerializeField] private float baseDame =1f;
    [SerializeField] private float baseHp = 1000f;
    [SerializeField] private float baseArmor = 5f;
    [SerializeField] private float baseMana = 10f;
    [SerializeField] private float manaRecovery = 1f;


    // Getter cho các thuộc tính
    public ShipCode ShipCode => shipCode;
    public ShipName ShipName => shipName;
    public TypeObject TypeObject => typeObject;
    public Sprite Sprite => sprite;
    public float BaseDame => baseDame;
    public float MaxHP => baseHp;
    public float Mana => baseMana;
    public float ManaRecovery => manaRecovery;

    // Cache list các profile đã load để tăng hiệu suất
    private static List<ShipProfileSO> cachedProfiles;
    private void OnEnable()
    {
        this.SetBaseValue(this.originalDame, this.originalMaxHP,this.originalArmor, this.originalMana, this.originalManaRecovery);
    }
    public static ShipProfileSO FindByShipCode(ShipCode shipCode)
    {
        var profiles = Resources.LoadAll("ShootableObject/Ship/ShipProfile", typeof(ShipProfileSO));
        foreach (ShipProfileSO profile in profiles)
        {
            if (profile.shipCode != shipCode) continue;
            return profile;
        }
        return null;
    }
    // Lấy tất cả các profile
    public static List<ShipProfileSO> GetAllShipProfile()
    {
        if (cachedProfiles == null || cachedProfiles.Count == 0)
        {
            cachedProfiles = new List<ShipProfileSO>(Resources.LoadAll<ShipProfileSO>("ShootableObject/Ship/ShipProfile"));
        }

        return cachedProfiles;
    }
    public virtual float UpdateBaseDame(float increase)
    {
        return this.baseDame =+ Mathf.Max(0, baseDame + increase);
    }
    public virtual float UpdateBaseHp(float increase)
    {
        return this.baseHp =+ Mathf.Max(0, baseHp + increase);
    }
    public virtual float UpdateBaseMana(float increase)
    {
        return this.baseMana =+ Mathf.Max(0, baseMana + increase);
    }
    public virtual float UpdateManaRecovery(float increase)
    {
        return this.manaRecovery =+ Mathf.Max(0, manaRecovery + increase);
    }
    public virtual float GetBaseArmor()
    {
        return this.baseArmor;
    }
    public virtual float UpdateBaseArmor(float increase)
    {
        return this.baseArmor = +Mathf.Max(0, baseArmor + increase);
    }
    private void SetBaseValue(float originalDame, float originalMaxHP, float originalArmor, float originalMana, float originalManaRecovery)
    {
        this.baseDame = originalDame;
        this.baseHp = originalMaxHP;
        this.baseArmor = originalArmor;
        this.baseMana = originalMana;   
        this.manaRecovery = originalManaRecovery;
    }

}
