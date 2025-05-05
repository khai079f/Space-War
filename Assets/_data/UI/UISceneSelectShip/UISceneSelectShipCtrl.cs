using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneSelectShipCtrl :GameMonoBehaviour
{
    [SerializeField] protected UIShipSelectCtrl shipSelectCtrl;
    public UIShipSelectCtrl ShipSelectCtrl => shipSelectCtrl;
    [SerializeField] protected UIShowInfoShipSelected showInfoShipSelected;
    public UIShowInfoShipSelected ShowInfoShipSelected => showInfoShipSelected;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIShipSelectCtrl();
        this.LoadUIShowInfoShipSelected();
    }
    protected virtual void LoadUIShipSelectCtrl()
    {
        if (shipSelectCtrl != null) return;
        this.shipSelectCtrl = transform.Find("UICenter").Find("UISelectCharacterCtrl").GetComponent<UIShipSelectCtrl>();
        Debug.LogWarning(transform.name + ": LoadUIShipSelectCtrl ", gameObject);
    }
    protected virtual void LoadUIShowInfoShipSelected()
    {
        if (showInfoShipSelected != null) return;
        this.showInfoShipSelected = transform.Find("UILeft").Find("UIInformationShipSelected").GetComponent<UIShowInfoShipSelected>();
        Debug.LogWarning(transform.name + ": LoadUIShowInfoShipSelected ", gameObject);
    }
}
