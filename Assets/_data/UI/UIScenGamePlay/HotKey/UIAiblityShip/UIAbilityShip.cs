using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class UIAbilityShip : GameMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image image;
    [SerializeField] protected UITooltip tooltip;
    [SerializeField] protected BaseAbility baseAbility;
    public BaseAbility BaseAbility => baseAbility;
    protected override void Start()
    {
        base.Start();
        this.UpdateReactPos();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImage();
    }
    public virtual void SetUISprite(BaseAbility abilityChoosed)
    {
            this.image.sprite = abilityChoosed.AbilityData.sprite;
    }

    protected virtual void LoadImage()
    {
        if (image != null) return;
        this.image = GetComponent<Image>();
        Debug.LogWarning(transform.name+":LoadImage",gameObject);
    }
    protected virtual void LoadUITooltip()
    {
        if (this.tooltip != null) return;
        this.tooltip =transform.Find("TooltipContainer").GetComponent<UITooltip>();
        if(this.tooltip == null) Debug.LogWarning(transform.name + ":cant able LoadUITooltip", gameObject);

    }
    protected virtual void UpdateReactPos()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3 newLocalPos = rectTransform.localPosition;
        newLocalPos.z = 0f; // Đặt PosZ về 0
        rectTransform.localPosition = newLocalPos;
    }
    public virtual void SetAbilitySkill(BaseAbility abilityChoosed)
    {
        // Cập nhật baseAbility với lớp con
        this.baseAbility = abilityChoosed;
        // Gọi phương thức SetAbilitySkill từ BaseAbility
    }
    public virtual void OnEnableSkill()
    {

        this.baseAbility.UseAbilitySkill();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.LoadUITooltip();
        this.tooltip.ShowTooltip(); // Show tooltip when mouse enters
    }

    // Khi chuột rời khỏi đối tượng
    public void OnPointerExit(PointerEventData eventData)
    {
        this.tooltip.HideTooltip(); // Hide tooltip when mouse exits
    }
}
