using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContentEffects : UIEffects
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float hoverScale = 1.1f;
    private Vector3 originalScale;

    protected override void Start()
    {
        // Lưu kích thước gốc
        originalScale = rectTransform.localScale;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRectTransform();
    }

    protected virtual void LoadRectTransform()
    {
        if (rectTransform != null) return;
        this.rectTransform = GetComponent<RectTransform>();
        Debug.LogWarning(transform.name + ": LoadRectTransform ", gameObject);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.transform.localScale = originalScale * hoverScale;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.transform.localScale = originalScale;
    }
}
