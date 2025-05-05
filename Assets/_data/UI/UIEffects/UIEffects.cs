using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIEffects : GameMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);

}
