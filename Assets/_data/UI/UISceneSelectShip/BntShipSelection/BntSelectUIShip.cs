using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BntSelectUIShip : BaseButton
{
    [Header("Base Button")]
    [SerializeField] protected UIShip uiShip;
    [SerializeField] protected UIShipSelectCtrl shipSelectCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIShip();
        this.LoadUIShipSelectCtrl();
    }
    protected virtual void LoadUIShip()
    {
        if (this.uiShip != null) return;
        this.uiShip = GetComponent<UIShip>();
        Debug.LogWarning(transform.name + " LoadUIShip", gameObject);
    }
    protected virtual void LoadUIShipSelectCtrl()
    {
        if (this.shipSelectCtrl != null) return;
        this.shipSelectCtrl = GetComponentInParent<UIShipSelectCtrl>();
        Debug.LogWarning(transform.name + " LoadUIShipSelectCtrl", gameObject);
    }
    protected override void OnClick()
    {
        // Show InfoShip Selected:
        this.shipSelectCtrl.SceneSelectShipCtrl.ShowInfoShipSelected.ShowShipSelected(this.uiShip.GetInfoShip());
        // Lưu thông tin tàu đã chọn vào ShipSelectionData
        ShipSelectionData.Instance.SetSelectedShip(this.uiShip.ShipProfile);
        //Debug.Log(ShipSelectionData.Instance.GetSelectedShip());
    }
}
