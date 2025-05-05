using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyAbilitiesShipSpawner : Spawner
{
    [Header("UI Key Abilities Ship Spawner")]
    protected static UIKeyAbilitiesShipSpawner instance;
    public static UIKeyAbilitiesShipSpawner Instance => instance;

    protected string UIAbility = "UIAbility";
    protected string PressAbleAbility = "PressAbleAbility";
    protected string FilledCooldown = "FilledCooldown";
    protected string Stack = "Stack";
    protected string TooltipContainer = "TooltipContainer";
    [SerializeField] protected UIHotKeyCtrl hotKeyCtrl;
    [SerializeField] protected UIPrefabsAbilitiActive prefabsAbilitiActive;
    public UIHotKeyCtrl HotKeyCtrl => hotKeyCtrl;

    protected override void Awake()
    {
        base.Awake();
        if (UIKeyAbilitiesShipSpawner.instance != null) Debug.LogWarning("Only 1 UIKeyAbilitiesShipSpawner allow to exist");
        UIKeyAbilitiesShipSpawner.instance = this;
    }
    protected override void Start()
    {
        base.Start();
        this.prefabsAbilitiActive.OnBaseAbilitiesUpdated += SpawningAbility;
        this.SpawningAbility(this.hotKeyCtrl.AbilitiActive.AbilityDatas);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.prefabsAbilitiActive.OnBaseAbilitiesUpdated -= SpawningAbility;
    }
    protected override void LoadComponents()
    {
        this.LoadUIHotKeyCtrl();
        base.LoadComponents();
        this.LoadUIPrefabsAbilitiActive();

    }
    protected override void LoadHolder()
    {

        if (this.holder != null) this.holder = null; // Nếu holder đã được set thì thoát

        this.holder = this.FindEmptyItemSlot(); // Tìm slot rỗng làm holder
        if (this.holder == null) Debug.LogWarning(transform.name + ": No empty item slot available", gameObject);
    }

    protected virtual void LoadUIHotKeyCtrl()
    {
        if (this.hotKeyCtrl != null) return;
        this.hotKeyCtrl = transform.GetComponentInParent<UIHotKeyCtrl>();
        Debug.Log(transform.name + ": LoadUIInventoryCtrl", gameObject);
    }
    protected virtual void LoadUIPrefabsAbilitiActive()
    {
        if (this.prefabsAbilitiActive != null) return;
        this.prefabsAbilitiActive = transform.Find("Prefabs").GetComponent<UIPrefabsAbilitiActive>();
        Debug.Log(transform.name + ": LoadUIPrefabsAbilitiActive", gameObject);
    }
    protected virtual Transform FindEmptyItemSlot()
    {
        foreach (ItemSlot slot in this.hotKeyCtrl.itemSlots)
        {
            if (slot.transform.childCount == 0) // Kiểm tra nếu không có child
            {
                return slot.transform;
            }
        }
        return null; // Trả về null nếu không tìm thấy slot rỗng
    }

    public virtual void SpawningAbility(List<AbilityShipAbstract> AbilityDatas)
    {

       for (int i =0; i < AbilityDatas.Count; i++)
        {
            if (this.CheckIsChoosedAbility(AbilityDatas[i])) continue;
            this.hotKeyCtrl.AbilitiActive.SetIsChoose(AbilityDatas[i]);
            this.SpawnHotKeyAbility(AbilityDatas[i].AbilityData);
        }
    }

    protected virtual void SpawnHotKeyAbility(AbilityData abilityData)
    {
        this.LoadHolder();
        Transform uiItem = this.Spawn(UIKeyAbilitiesShipSpawner.Instance.UIAbility, Vector3.zero, Quaternion.identity);
        uiItem.transform.localScale = new Vector3(1, 1, 1);
        uiItem.gameObject.SetActive(true);
        this.SpawnPressAbleAbility(uiItem);
        this.SpawnTooltip(uiItem);
        if (this.CheckIsActiveSkill(abilityData) == TypeObject.Stack)
        {
            this.SpawnStack(uiItem);
            return;
        } 
        this.SpawnFilledCooldown(uiItem);
        
    }
    protected virtual bool CheckIsChoosedAbility( BaseAbility abilityData)
    {
        return abilityData.isChoose;
    }
    protected virtual void SpawnPressAbleAbility(Transform parent)
    {
        Transform PressAble = this.Spawn(UIKeyAbilitiesShipSpawner.Instance.PressAbleAbility, Vector3.zero, Quaternion.identity);
        PressAble.transform.localScale = new Vector3(1, 1, 1);
        PressAble.gameObject.SetActive(true);
        PressAble.SetParent(parent);
    }
    protected virtual void SpawnFilledCooldown(Transform parent)
    {
        Transform PressAble = this.Spawn(UIKeyAbilitiesShipSpawner.Instance.FilledCooldown, Vector3.zero, Quaternion.identity);
        PressAble.transform.localScale = new Vector3(1, 1, 1);
        PressAble.gameObject.SetActive(true);
        PressAble.SetParent(parent);
    }
    protected virtual void SpawnStack(Transform parent)
    {
        Transform PressAble = this.Spawn(UIKeyAbilitiesShipSpawner.Instance.Stack, Vector3.zero, Quaternion.identity);
        PressAble.transform.localScale = new Vector3(1, 1, 1);
        PressAble.gameObject.SetActive(true);
        PressAble.SetParent(parent);
    }
    protected virtual void SpawnTooltip(Transform parent)
    {
        Transform PressAble = this.Spawn(UIKeyAbilitiesShipSpawner.Instance.TooltipContainer, Vector3.zero, Quaternion.identity);
        PressAble.transform.localScale = new Vector3(1, 1, 1);
        PressAble.gameObject.SetActive(true);
        PressAble.SetParent(parent);
    }
    protected virtual TypeObject CheckIsActiveSkill(AbilityData abilityData)
    {
        return abilityData.typeObject;
    }
}

