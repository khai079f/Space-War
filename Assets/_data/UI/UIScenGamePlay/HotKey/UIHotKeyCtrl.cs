using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHotKeyCtrl : GameMonoBehaviour
{
    private static UIHotKeyCtrl _instance;
    public static UIHotKeyCtrl instance => _instance;

    public List<ItemSlot> itemSlots;
    [SerializeField] protected UIKeyAbilitiesShipSpawner abilitiesShipSpawner;
    public UIKeyAbilitiesShipSpawner AbilitiesShipSpawner => abilitiesShipSpawner;
    [SerializeField] protected UIPrefabsAbilitiActive abilitiActive;
    public UIPrefabsAbilitiActive AbilitiActive => abilitiActive;

    protected override void Awake()
    {
        if (UIHotKeyCtrl._instance != null) Debug.LogError("only 1 UIHotKeyCtrl in secen");
        UIHotKeyCtrl._instance = this;

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemSlots();
        this.LoadAbilitiesShipSpawner();
        this.LoadAbilitiActive();
    }
    protected virtual void LoadItemSlots()
    {
        if (this.itemSlots.Count >0) return;
        ItemSlot[] arraySlot = GetComponentsInChildren<ItemSlot>();
        this.itemSlots.AddRange(arraySlot);
        Debug.LogWarning(transform.name + ": LoadItemSlots from UI", gameObject);
    }
    protected virtual void LoadAbilitiesShipSpawner()
    {
        if (this.abilitiesShipSpawner != null) return;
        this.abilitiesShipSpawner = GetComponentInChildren<UIKeyAbilitiesShipSpawner>();
        Debug.LogWarning(transform.name + ": LoadAbilitiesShipSpawner from UI", gameObject);
    }
    protected virtual void LoadAbilitiActive()
    {
        if (this.abilitiActive != null) return;
        this.abilitiActive = GetComponentInChildren<UIPrefabsAbilitiActive>();
        Debug.LogWarning(transform.name + ": LoadAbilitiActive from UI", gameObject);
    }

}

