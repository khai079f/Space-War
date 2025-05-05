using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGlobalPlayerCtrl : GameMonoBehaviour
{
    [SerializeField] protected List<AbilityShipAbstract> baseShipAbilitys;
    public List<AbilityShipAbstract> BaseShipAbilitys => baseShipAbilitys;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBaseAbility();
    }
    public virtual void LoadBaseAbility()
    {
        if (this.baseShipAbilitys.Count > 0) this.baseShipAbilitys.Clear();

        foreach (Transform child in this.transform.GetComponentsInChildren<Transform>(true))
        {
            AbilityShipAbstract ability = child.GetComponent<AbilityShipAbstract>();
            if (ability != null)
            {
                this.baseShipAbilitys.Add(ability);
            }
        }
        this.CheckAbleAbility(this.baseShipAbilitys);
        this.DisableAllBaseAbilities();
    }
    protected virtual void DisableAllBaseAbilities()
    {
        foreach (var ability in this.baseShipAbilitys)
        {
            ability.gameObject.SetActive(false); // Tắt game object của từng khả năng
        }
    }
    protected virtual void CheckAbleAbility(List<AbilityShipAbstract> baseShipAbilitys)
    {
        foreach(AbilityShipAbstract shipAbstract in baseShipAbilitys)
        {
            if (shipAbstract.ShipPlayAbleAbilities != null) shipAbstract.LoadShipPlayAbleAbilities();
        }
    }
}
