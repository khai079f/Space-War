using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : GameMonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropObj = eventData.pointerDrag;
        DragItem dragItem = dropObj.GetComponent<DragItem>();

        if (transform.childCount == 0)
        {
            this.MoveItemToSlot(dragItem, transform);  // Slot trống, di chuyển item vào
        }
        else
        {
            this.SwapItems(dragItem);  // Slot đã có item, hoán đổi
        }
    }

    // Phương thức chung để di chuyển item vào slot mới
    protected virtual void MoveItemToSlot(DragItem dragItem, Transform newParent)
    {
        dragItem.SetRealParent(newParent);  // Cập nhật parent
        dragItem.transform.SetParent(newParent);  // Di chuyển item vào slot mới
    }

    // Hoán đổi item giữa hai slot
    protected virtual void SwapItems(DragItem draggedItem)
    {
        // Lấy item hiện có trong slot
        GameObject currentItem = transform.GetChild(0).gameObject;
        DragItem currentDragItem = currentItem.GetComponent<DragItem>();

        // Di chuyển item hiện có về parent gốc
        Transform originalParent = draggedItem.GetOriginalParent();
        this.MoveItemToSlot(currentDragItem, originalParent);  // Di chuyển item cũ về

        // Di chuyển item mới vào slot hiện tại
        this.MoveItemToSlot(draggedItem, transform);
    }
}
