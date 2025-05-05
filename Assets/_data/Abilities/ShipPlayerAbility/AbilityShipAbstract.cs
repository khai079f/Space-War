using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityShipAbstract : BaseAbilityLevelUp
{
    [Header("Ability ShipAbstract")]
    [SerializeField] protected ShipPlayAbleAbilities shipPlayAbleAbilities;
    public ShipPlayAbleAbilities ShipPlayAbleAbilities => shipPlayAbleAbilities;

    protected override void Start()
    {
        base.Start();
        this.delay = this.abilityData.Cooldown;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipPlayAbleAbilities();
    }
    public virtual void LoadShipPlayAbleAbilities()
    {
        if (this.shipPlayAbleAbilities != null && this.abilityData.typeObject != TypeObject.GlobalAbility) return;
        if (transform.GetComponentInParent<ShipPlayAbleAbilities>() == null)
        {
            this.shipPlayAbleAbilities = null;
            return;
        }
        this.shipPlayAbleAbilities = transform.GetComponentInParent<ShipPlayAbleAbilities>();
        // Debug.LogWarning(transform.name + ": LoadAbilities", gameObject);
        
    }

    protected override void LoadAbilityData()
    {
        if (this.abilityData != null) return;
        string resPath = "ShootableObject/Ship/Ability/" + transform.name;
        Debug.Log(resPath);
        this.abilityData = Resources.Load<AbilityData>(resPath);
        Debug.LogWarning(transform.name + ": LoadAbilityData", gameObject);
    }
    protected virtual void LoadAbility<T>(ref T component,string nameObject, string componentName) where T : Component
    {
        if (component != null) return;
        if (!transform.Find(nameObject)) return ;
        component = transform.Find(nameObject).GetComponent<T>();
        Debug.LogWarning(transform.name + ": Loaded " + componentName, gameObject);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.abilityData.SetBaseValue();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.abilityData.SetBaseValue();
    }
}
