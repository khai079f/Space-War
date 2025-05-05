using UnityEngine;
using UnityEngine.UI;

public class UIItemInventory : GameMonoBehaviour
{
    [Header("UI Item Inventory")]
    [SerializeField] protected ItemInventory itemInventory;
    public ItemInventory ItemInventory => itemInventory;
    [SerializeField] protected Text itemNumber;
    public Text ItemNumber => itemNumber;
    [SerializeField] protected Text itemName;
    public Text ItemName => itemName;
    [SerializeField] protected Image itemSprite;
    public Image ItemSprite => itemSprite;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemName();
        this.LoadItemNumber();
        this.LoadItemSprite();

    }

    protected virtual void LoadItemName()
    {
        if (itemName != null) return;
        this.itemName = transform.Find("ItemName").GetComponent<Text>();
        Debug.LogWarning(transform.name + ": LoadItemName ", gameObject);
    }
    protected virtual void LoadItemNumber()
    {
        if (itemNumber != null) return;
        this.itemNumber = transform.Find("ItemNumber").GetComponent<Text>();
        Debug.LogWarning(transform.name + ": LoadItemNumber ", gameObject);
    }
    protected virtual void LoadItemSprite()
    {
        if (itemSprite != null) return;
        this.itemSprite = transform.Find("ItemSprite").GetComponent<Image>();
        this.itemSprite.rectTransform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        Debug.LogWarning(transform.name + ": LoadItemSprite ", gameObject);
    }

    public virtual void ShowItem(ItemInventory item)
    {
        this.itemInventory = item;
        this.itemSprite.sprite = this.itemInventory.itemProfile.sprite;
        this.itemName.text = this.itemInventory.itemProfile.itemName.ToString();
        this.itemNumber.text = this.itemInventory.itemCount.ToString();
    }
}