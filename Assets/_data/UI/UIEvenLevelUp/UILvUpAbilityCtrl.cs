using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILvUpAbilityCtrl : GameMonoBehaviour
{
    [SerializeField] protected Image image;
    public Image Image => image;
    [SerializeField] protected TextMeshProUGUI nameAbility;
    public TextMeshProUGUI NameAbility => nameAbility;
    [SerializeField] protected TextMeshProUGUI description;
    public TextMeshProUGUI Description => description;
    [SerializeField] protected AbilityData abilityData;
    public AbilityData AbilityData => abilityData;
    protected AbilityShipAbstract shipAbility;
    public AbilityShipAbstract ShipAbility => shipAbility;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponentInChildren(ref this.image, "Image", "Image");
        this.LoadComponentInChildren(ref this.nameAbility, "TextMeshProUGUI", "NameAbility");
        this.LoadComponentInChildren(ref this.description, "TextMeshProUGUI", "Description");
    }
    protected virtual void LoadComponentInChildren<T>(ref T component, string componentName, string NameObj) where T : Component
    {
        if (component != null) return;
        component = transform.Find(NameObj).GetComponent<T>();
        Debug.LogWarning(transform.name + ": Loaded " + componentName, gameObject);
    }
    public virtual void SetDataForUIAbility(AbilityData abilityData, AbilityShipAbstract BaseAbility)
    {
        this.shipAbility = BaseAbility;
        this.abilityData = abilityData;
        this.image.sprite = abilityData.sprite;
        this.nameAbility.text = abilityData.nameAbility;
        this.description.text = abilityData.contentLevelup.description;
    }
}
