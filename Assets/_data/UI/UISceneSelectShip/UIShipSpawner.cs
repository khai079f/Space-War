using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShipSpawner : Spawner
{
    protected static UIShipSpawner instance;
    public static UIShipSpawner Instance => instance;

    public static string normalShip = "UIShip";

    [Header("UI Ship Spawner")]
    [SerializeField] protected UIShipSelectCtrl shipSelectCtrl;
    public UIShipSelectCtrl UIShipSelectCtrl => shipSelectCtrl;

    protected override void Awake()
    {
        base.Awake();
        if (UIShipSpawner.instance != null) Debug.LogWarning("Only 1 UIShipSpawner allow to exist");
        UIShipSpawner.instance = this;
    }
    protected override void LoadHolder()
    {
        this.LoadUIInventoryCtrl();
        if (this.holder != null) return;
        this.holder = this.shipSelectCtrl.Content;
        Debug.Log(transform.name + ": LoadHolder", gameObject);

    }

    protected virtual void LoadUIInventoryCtrl()
    {
        if (this.shipSelectCtrl != null) return;
        this.shipSelectCtrl = transform.GetComponentInParent<UIShipSelectCtrl>();
        Debug.Log(transform.name + ": LoadUIInventoryCtrl", gameObject);
    }

    public virtual void ClearItems()
    {
        foreach (Transform item in this.holder)
        {
            this.Despawn(item);
        }
    }

    public virtual void SpawnShip(ShipProfileSO ship)
    {
        Transform uiShip = this.shipSelectCtrl.UIShipSpawner.Spawn(UIShipSpawner.normalShip, Vector3.zero, Quaternion.identity);
        uiShip.transform.localScale = new Vector3(1, 1, 1);

        // Đảm bảo pos z = 0 cho RectTransform
        RectTransform rectTransform = uiShip.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector3 position = rectTransform.localPosition;
            position.z = 0;
            rectTransform.localPosition = position;
        }

        UIShip shipSelection = uiShip.GetComponent<UIShip>();
        shipSelection.ShowShip(ship);
        uiShip.gameObject.SetActive(true);
    }
}
