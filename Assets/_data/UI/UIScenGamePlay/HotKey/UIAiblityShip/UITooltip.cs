using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITooltip : UIAbilityHotKeyAbstract
{
    [SerializeField] protected Text textDescription;
    [SerializeField] protected Image backGround;

    protected override void Start()
    {
        base.Start();
        HideTooltip();// Hide the tooltip by default
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIText();
        this.LoadBackGround();
    }
    protected virtual void LoadUIText()
    {
        if (textDescription != null) return;
        this.textDescription = transform.Find("TooltipText").GetComponent<Text>();
        Debug.LogWarning(transform.name + " LoadUIText ", gameObject);
    }
    protected virtual void LoadBackGround()
    {
        if (backGround != null) return;
        this.backGround = transform.Find("BackGround").GetComponent<Image>();
        Debug.LogWarning(transform.name + " LoadUIText ", gameObject);
    }

    protected virtual void SetTooltipForAbility()
    {
        if (this.UIAbilityShip == null || this.UIAbilityShip.BaseAbility == null) return;
        this.textDescription.text = this.UIAbilityShip.BaseAbility.AbilityData.Description;
    }

    public virtual void ShowTooltip()
    {
        this.SetTooltipForAbility();
        textDescription.gameObject.SetActive(true); // Show the current GameObject (TooltipContainer)
    }

    public virtual void HideTooltip()
    {
        textDescription.gameObject.SetActive(false); // Hide the current GameObject (TooltipContainer)
    }

}
