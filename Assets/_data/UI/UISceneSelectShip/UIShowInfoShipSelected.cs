using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIShowInfoShipSelected : GameMonoBehaviour
{
    [Header("UI Show InfoShip Selected")]
    [SerializeField] protected UISceneSelectShipCtrl sceneSelectShipCtrl;
    [SerializeField] protected Text shipName;
    public Text ShipName => shipName;
    [SerializeField] protected Image shipSprite;
    public Image ShipSprite => shipSprite;
    protected override void Start()
    {
        base.Start();
        this.ShowShipDefault();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipName();
        this.LoadShipSprite();
        this.LoadsceneSelectShipCtrl();
    }

    protected virtual void LoadShipName()
    {
        if (shipName != null) return;
        this.shipName = transform.Find("ShipName").GetComponent<Text>();
        Debug.LogWarning(transform.name + ": LoadShipName ", gameObject);
    }

    protected virtual void LoadShipSprite()
    {
        if (shipSprite != null) return;
        this.shipSprite = transform.Find("ShipSprite").GetComponent<Image>();
        this.shipSprite.rectTransform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        Debug.LogWarning(transform.name + ": LoadShipSprite ", gameObject);
    }
    protected virtual void LoadsceneSelectShipCtrl()
    {
        if (sceneSelectShipCtrl != null) return;
        this.sceneSelectShipCtrl = transform.GetComponentInParent<UISceneSelectShipCtrl>();
        Debug.LogWarning(transform.name + ": LoadsceneSelectShipCtrl ", gameObject);
    }

    public virtual void ShowShipSelected(ShipProfileSO ship)
    {
        if (ship == null)
        {
            Debug.LogError("Ship no info");
            return;
        }
        this.ShipSprite.sprite = ship.Sprite;
        this.shipName.text = ship.ShipName.ToString();
    }

    protected virtual void ShowShipDefault()
    {
        List<ShipProfileSO> ships = ShipProfileSO.GetAllShipProfile();
        if(ships == null)
        {
            Debug.LogWarning("ship emty");
            return;
        }
        this.ShipSprite.sprite = ships[0].Sprite;
        this.shipName.text = ships[0].ShipName.ToString();
    }
}