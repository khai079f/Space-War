using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrefabsAbilitiActive : UIAbstractAbilityShip
{
    [SerializeField] protected UIAbilityShip abilityShipUI;
    [SerializeField] protected List<AbilityShipAbstract> abilityDatas;
    public List<AbilityShipAbstract> AbilityDatas => abilityDatas;
    public event Action<List<AbilityShipAbstract>> OnBaseAbilitiesUpdated;
    protected override void Start()
    {
        base.Start();
        this.CurrentShip.ShipAbilities.OnBaseAbilitiesUpdated += GetAllAbility;
        this.GetAllAbility(this.CurrentShip.ShipAbilities.BaseShipAbilitys);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.CurrentShip.ShipAbilities.OnBaseAbilitiesUpdated -= GetAllAbility;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIAbilityShip();
    }

    protected virtual void LoadUIAbilityShip()
    {
        if (this.abilityShipUI != null) return;
        this.abilityShipUI = transform.Find("UIAbility").GetComponent<UIAbilityShip>();
        Debug.LogWarning(transform.name + ": loadUIAbilityShip", gameObject);
    }

    protected virtual void GetAllAbility(List<AbilityShipAbstract> abilityDatas)
    {
        
        if (this.CurrentShip == null) return;
        if (this.CurrentShip.ShipAbilities.BaseShipAbilitys.Count <= 0) return;
       // Debug.Log("Count:" + this.CurrentShip.ShipAbilities.BaseAbility.Count);
        this.abilityDatas = abilityDatas;
        this.OnBaseAbilitiesUpdated?.Invoke(this.abilityDatas);
    }

    public virtual void SetIsChoose(BaseAbility abilityChoosed)
    {
        foreach(BaseAbility abilityData in this.abilityDatas)
        {
            if (abilityData == abilityChoosed)
            {
                abilityShipUI.SetUISprite(abilityChoosed);
                abilityData.isChoose = true;
                abilityShipUI.SetAbilitySkill(abilityChoosed);
            }
           
        }
    }
}
