using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextEffects : UIEffects
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float hoverScale = 1.2f;
    private Vector3 originalScale;

    protected override void Start()
    {
        // Lưu kích thước gốc
        originalScale = text.transform.localScale;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextMeshProUGUI();
    }

    protected virtual void LoadTextMeshProUGUI()
    {
        if (text != null) return;
        this.text = GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTextMeshProUGUI ", gameObject);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        text.transform.localScale = originalScale * hoverScale;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        text.transform.localScale = originalScale;
    }
}
