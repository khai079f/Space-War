using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvenLvCtrl : GameMonoBehaviour
{
    [SerializeField] protected ShipCtrl CurrentShip;
    [SerializeField] protected List<AbilityShipAbstract> abilityDatas;
    public List<AbilityShipAbstract> AbilityDatas => abilityDatas;
    [SerializeField] protected List<UILvUpAbilityCtrl> lvUpAbilityCtrls;

    protected override void OnEnable()
    {
        this.LoadCurrentShip();
        this.GetAllAbilityFromShip(); // Gọi hàm này khi CurrentShip đã sẵn sàng
        this.GetAllAbilityGlobal();
        this.LoadDataForUIAbility();

    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUILvUpAbilityCtrl();
    }
    protected virtual void LoadCurrentShip()
    {
        if (CurrentShip != null) return;
        if (PlayerCtrl.instance.CurrentShip == null) return;
        this.CurrentShip = PlayerCtrl.instance.CurrentShip;
        //Debug.Log(transform.name + ":LoadCurrentShip", gameObject);
    }
    protected virtual void GetAllAbilityFromShip()
    {
        if (this.CurrentShip == null) return;
        if (this.CurrentShip.ShipAbilities.BaseShipAbilitys.Count <= 0) return;
        // Debug.Log("Count:" + this.CurrentShip.ShipAbilities.BaseAbility.Count);
        this.abilityDatas.AddRange(this.CurrentShip.ShipAbilities.BaseShipAbilitys);
        // Thông báo đến Mediator rằng dữ liệu khả năng đã được cập nhật
    }
    protected virtual void GetAllAbilityGlobal()
    {
        if (this.CurrentShip == null) return;
        if (PlayerCtrl.instance.AbilityGlobalPlayer.BaseShipAbilitys.Count <= 0 || this.CurrentShip.ShipAbilities.BaseShipAbilitys.Count == 7 ) return;
        PlayerCtrl.instance.AbilityGlobalPlayer.LoadBaseAbility();
        // Debug.Log("Count:" + this.CurrentShip.ShipAbilities.BaseAbility.Count);
        this.abilityDatas.AddRange(PlayerCtrl.instance.AbilityGlobalPlayer.BaseShipAbilitys);
        // Thông báo đến Mediator rằng dữ liệu khả năng đã được cập nhật
    }
    protected virtual void LoadUILvUpAbilityCtrl()
    {
        if (this.lvUpAbilityCtrls.Count > 0) this.lvUpAbilityCtrls.Clear();
        UILvUpAbilityCtrl[] lvUpAbilityCtrls = transform.GetComponentsInChildren<UILvUpAbilityCtrl>();
        this.lvUpAbilityCtrls.AddRange(lvUpAbilityCtrls);
        if(this.lvUpAbilityCtrls.Count <= 0) Debug.LogWarning(transform.name + ":Load All UILvUpAbilityCtrl Fail", gameObject);
    }

    protected virtual void LoadDataForUIAbility()
    {
        // Kiểm tra danh sách khả năng và UI có dữ liệu để xử lý
        if (this.abilityDatas.Count <= 0 || this.lvUpAbilityCtrls.Count <= 0)
            return;

        // Danh sách tạm để theo dõi các khả năng chưa chọn
        List<AbilityShipAbstract> remainingAbilities = new List<AbilityShipAbstract>(this.abilityDatas);
        remainingAbilities.RemoveAll(ability => ability.AbilityData.Lv >= 5);
        // Nếu không còn khả năng nào hợp lệ, dừng xử lý
        if (remainingAbilities.Count <= 0) return;
        if (remainingAbilities.Count < this.lvUpAbilityCtrls.Count)
        {
            this.lvUpAbilityCtrls[0].gameObject.SetActive(false);
            this.lvUpAbilityCtrls.RemoveAt(0);
        }
        // Đảm bảo danh sách không rỗng
        System.Random random = new System.Random();
       // Debug.Log("remainingAbilities:" + remainingAbilities.Count);
        for (int i = 0; i < this.lvUpAbilityCtrls.Count; i++)
        {
            if (remainingAbilities.Count <= 0) break; // Dừng nếu hết khả năng để chọn

            // Lấy ngẫu nhiên một Ability từ danh sách còn lại
            int randomIndex = random.Next(remainingAbilities.Count);
            //Debug.Log("randomIndex:" + randomIndex);
            AbilityShipAbstract selectedAbility = remainingAbilities[randomIndex];
            remainingAbilities.RemoveAt(randomIndex); // Loại bỏ khả năng đã chọn

            // Gán dữ liệu cho UI
            this.lvUpAbilityCtrls[i].SetDataForUIAbility(
                selectedAbility.AbilityData,
                selectedAbility
            );
        }
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.abilityDatas.Clear();
    }
}
