using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShipSelectCtrl : GameMonoBehaviour
{
    [SerializeField] protected Transform content;
    public Transform Content => content;
    [SerializeField] protected UIShipSpawner UIshipSpawner;
    public UIShipSpawner UIShipSpawner => UIshipSpawner;
    [SerializeField] protected UIShip uIShip;
    public UIShip UIShip => uIShip;
    [SerializeField] protected UISceneSelectShipCtrl sceneSelectShipCtrl;
    public UISceneSelectShipCtrl SceneSelectShipCtrl => sceneSelectShipCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadContent();
        this.LoadUIShipSpawner();
        this.LoadUISceneSelectShipCtrl();
        this.LoadUIShip();
    }

    protected virtual void LoadContent()
    {
        if (content != null) return;
        this.content = transform.Find("UIShipSelection").Find("Scroll View").Find("Viewport").Find("Content");
        Debug.LogWarning(transform.name + ": LoadContent ", gameObject);
    }
    protected virtual void LoadUIShipSpawner()
    {
        if (UIshipSpawner != null) return;
        this.UIshipSpawner = GetComponentInChildren<UIShipSpawner>();
        Debug.LogWarning(transform.name + ": LoadUIInventorySpawner ", gameObject);
    }

    protected virtual void LoadUISceneSelectShipCtrl()
    {
        if (sceneSelectShipCtrl != null) return;
        this.sceneSelectShipCtrl = GetComponentInParent<UISceneSelectShipCtrl>();
        Debug.LogWarning(transform.name + ": LoadUISceneSelectShipCtrl ", gameObject);
    }
    protected virtual void LoadUIShip()
    {
        if (uIShip != null) return;
        this.uIShip = GetComponentInChildren<UIShip>();
        Debug.LogWarning(transform.name + ": LoadUIShip ", gameObject);
    }
}