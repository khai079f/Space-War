using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class DragItem : GameMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] protected Image image;
    protected Transform realParent;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImage();
    }

    protected virtual void LoadImage()
    {
        if (this.image != null) return;
        this.image = GetComponent<Image>();
        //Debug.LogWarning(transform.name + "LoadImage", gameObject);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.realParent = transform.parent;
        //Debug.Log(realParent.name + ":GetOriginalParent");
        transform.SetParent(UIHotKeyCtrl.instance.transform);
        this.image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = InputManager.Instance.MouseWorldPos;
        mousePos.z = 0;
        transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(this.realParent);
        this.image.raycastTarget = true;
    }
    public virtual void SetRealParent(Transform transform)
    {
        this.realParent = transform;
    }
    public virtual Transform GetOriginalParent()
    {
        Transform originalParent = this.realParent;
        return originalParent;
    }
}
