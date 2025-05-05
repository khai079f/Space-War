using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIShip : GameMonoBehaviour
{
    [Header("UI Ship Selection")]
    [SerializeField] protected ShipProfileSO shipProfile;
    public ShipProfileSO ShipProfile => shipProfile;
    [SerializeField] protected Text shipName;
    public Text ShipName => shipName;
    [SerializeField] protected Image shipSprite;
    public Image ShipSprite => shipSprite;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipName();
        this.LoadShipSprite();
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
        //this.shipSprite.rectTransform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        Debug.LogWarning(transform.name + ": LoadShipSprite ", gameObject);
    }

    public virtual void ShowShip(ShipProfileSO ship)
    {
        this.shipProfile = ship;
        this.ShipSprite.sprite = this.shipProfile.Sprite;
        this.shipName.text = this.shipProfile.ShipName.ToString();
    }
    public virtual ShipProfileSO GetInfoShip()
    {
        if (this.shipProfile == null) {
            Debug.LogError("No any info Ship");
            return null;
        }
        return this.shipProfile;

    }
}