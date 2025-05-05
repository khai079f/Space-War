using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAblePlay : GameMonoBehaviour
{
    [SerializeField] protected List<Transform> shipAblePlays;
    [SerializeField] protected PlayerCtrl playerCtrl;
    public List<Transform> ShipAblePlays => shipAblePlays;
    //protected string shipPlayerSelected = "IroSpirit_Stealth";
    [SerializeField] protected ShipProfileSO shipPlayerSelected;

    protected override void Awake()
    {
        base.Awake();
        this.GetShipPlayerSelected();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
        this.LoadshipAblePlays();
    }
    protected virtual void LoadshipAblePlays()
    {
        if (this.shipAblePlays.Count > 0) this.shipAblePlays.Clear();
        Transform shipAblebObjs = GetComponent<Transform>();
        foreach (Transform ShipAble in shipAblebObjs)
        {
            this.shipAblePlays.Add(ShipAble);
        }
        this.HideShip();

        //Debug.Log(transform.name + ": LoadPrefabs", gameObject);
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GetComponentInParent<PlayerCtrl>();
        Debug.LogWarning(transform.name + " LoadPlayerCtrl", gameObject);
    }
    protected virtual void HideShip()
    {
        foreach (Transform shipAble in this.shipAblePlays)
        {
           
            shipAble.gameObject.SetActive(false);
        }
    }
    protected virtual void ActiveShip(Transform shipCurrent)
    {
        shipCurrent.gameObject.SetActive(true);
    }
    protected virtual void GetShipPlayerSelected()
    {
        this.shipPlayerSelected = ShipSelectionData.Instance.GetSelectedShip();
        if (shipPlayerSelected == null)
        {

            this.shipPlayerSelected = ShipSelectionData.Instance.GetDefaultShip();
            Debug.Log("shipPlayerSelected null");
        }
        foreach (Transform shipAble in this.shipAblePlays)
        {
            ShipCtrl shipCurrent = shipAble.transform.GetComponent<ShipCtrl>();
            if (shipCurrent == null)
            {
                Debug.LogWarning("shipPlayerSelected null");
                return;
            }
            ShipProfileSO checkShip = shipCurrent.ShipProfileSO;
            if(checkShip == this.shipPlayerSelected)
            {
                ActiveShip(shipAble);
                playerCtrl.GetCurrentShip();
            }
            
        }

    }

}
