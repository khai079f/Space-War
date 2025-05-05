using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIShipSelectAbstract : GameMonoBehaviour
{
    [Header("ShipSelect Abstract")]
    [SerializeField] protected UIShipSelectCtrl shipSelectCtrl;
    public UIShipSelectCtrl ShipSelectCtrl => shipSelectCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIShipSelectCtrl();

    }

    protected virtual void LoadUIShipSelectCtrl()
    {
        if (shipSelectCtrl != null) return;
        this.shipSelectCtrl = transform.parent.GetComponent<UIShipSelectCtrl>();
        Debug.LogWarning(transform.name + ": LoadUIShipSelectCtrl ", gameObject);
    }

}