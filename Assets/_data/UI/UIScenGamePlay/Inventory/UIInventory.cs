using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : UIInventoryAbstract
{
    // [Header("UI Inventory")]
    // private static UIInventory instance;
    // public static UIInventory Instance => instance;

    // protected bool isPaused = false; // Biến để lưu trạng thái pause
    // protected bool isOpen = false;
    // [SerializeField] protected UIInventorySort inventorySort = UIInventorySort.ByName;

    // protected override void Awake()
    // {
    //     base.Awake();
    //     if (UIInventory.instance != null) Debug.LogError("Only 1 UIInventory allow to exist");
    //     UIInventory.instance = this;
    // }

    // protected override void OnEnable()
    // {
    //     base.OnEnable();
    //     this.ClearItems();
    //     this.ShowItems();
    // }
    // public virtual void Toggle()
    // {
    //     this.isOpen = !this.isOpen;
    //     if (this.isOpen) this.Open();
    //     else this.Close();
    // }
    // public virtual void Pause()
    // {
    //     if (isPaused) return; // Nếu game đã tạm dừng, không cần tạm dừng lại
    //     Time.timeScale = 0f; // Dừng game
    //     isPaused = true;
    //     Debug.Log("Game is paused.");
    // }

    // public virtual void Resume()
    // {
    //     if (!isPaused) return; // Nếu game chưa tạm dừng, không cần tiếp tục lại
    //     Time.timeScale = 1f; // Tiếp tục game
    //     isPaused = false;
    //     Debug.Log("Game is resumed.");
    // }

    // public virtual void Open()
    // {
    //     if (!gameObject.activeSelf)
    //     {
    //         gameObject.SetActive(true);
    //         this.Pause(); // Tạm dừng game khi mở Inventory
    //     }
    // }

    // public virtual void Close()
    // {
    //     if (gameObject.activeSelf)
    //     {
    //         gameObject.SetActive(false);
    //         this.Resume(); // Tiếp tục game khi đóng Inventory
    //     }
    // }

    // public virtual void ShowItems()
    // {
    //     if (!isOpen) return;
    //     List<ItemInventory> items = PlayerCtrl.instance.CurrentShip.Itemlooter.Items;
    //     UIInventorySpawner spawner = this.inventoryCtrl.InventorySpawner;
    //     foreach (ItemInventory item in items)
    //     {
    //         spawner.SpawnItem(item);
    //     }
    //     this.SortItems();
    // }

    // protected virtual void ClearItems()
    // {
    //     this.inventoryCtrl.InventorySpawner.ClearItems();
    // }

    // protected virtual void SortItems()
    // {
    //     List<Transform> inventoryItems = new List<Transform>();

    //     // Lấy tất cả các item từ Content và thêm vào danh sách inventoryItems
    //     foreach (Transform itemTransform in this.inventoryCtrl.Content)
    //     {
    //         inventoryItems.Add(itemTransform);
    //     }

    //     // Sắp xếp danh sách dựa trên kiểu sắp xếp
    //     switch (this.inventorySort)
    //     {
    //         case UIInventorySort.ByName:
    //             inventoryItems.Sort((firstItemTransform, secondItemTransform) =>
    //             {
    //                 UIItemInventory firstUIItem = firstItemTransform.GetComponent<UIItemInventory>();
    //                 UIItemInventory secondUIItem = secondItemTransform.GetComponent<UIItemInventory>();

    //                 return string.Compare(firstUIItem.ItemInventory.itemProfile.itemName.ToString(), secondUIItem.ItemInventory.itemProfile.itemName.ToString());
    //             });
    //             break;

    //         case UIInventorySort.ByCount:
    //             inventoryItems.Sort((firstItemTransform, secondItemTransform) =>
    //             {
    //                 UIItemInventory firstUIItem = firstItemTransform.GetComponent<UIItemInventory>();
    //                 UIItemInventory secondUIItem = secondItemTransform.GetComponent<UIItemInventory>();

    //                 return firstUIItem.ItemInventory.itemCount.CompareTo(secondUIItem.ItemInventory.itemCount);
    //             });
    //             break;

    //         default:
    //             Debug.Log("UIInventorySort NoSort");
    //             break;
    //     }

    //     // Cập nhật lại thứ tự của các item trong UI dựa trên danh sách đã sắp xếp
    //     for (int index = 0; index < inventoryItems.Count; index++)
    //     {
    //         inventoryItems[index].SetSiblingIndex(index);
    //     }
    // }

}
