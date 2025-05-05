using UnityEngine;

public class BtnSelectAbilityLvUp : BaseButton
{
    [SerializeField] protected UILvUpAbilityCtrl uILvUpAbility;
    private bool canClick = false;
    private float timeActivated;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIEvenLvCtrl();
    }

    protected virtual void LoadUIEvenLvCtrl()
    {
        if (this.uILvUpAbility != null) return;
        this.uILvUpAbility = transform.GetComponent<UILvUpAbilityCtrl>();
        Debug.LogWarning(transform.name + ": LoadUIEvenLvCtrl", gameObject);
    }

    protected override void OnEnable()
    {
        timeActivated = Time.realtimeSinceStartup; // Lưu thời gian thực khi nút được bật
        canClick = false;
    }

    protected override void OnClick()
    {
        if (!canClick && Time.realtimeSinceStartup - timeActivated < 1f) return; // Kiểm tra độ trễ 1 giây

        canClick = true; // Cho phép nhấp vào nút sau khi đã qua 1 giây
        this.CheckAbilityGlobal(this.uILvUpAbility.ShipAbility);
        if (this.uILvUpAbility.ShipAbility.levelUpStrategy == null) this.uILvUpAbility.ShipAbility.WhatlevelUpStrategy();
        this.uILvUpAbility.AbilityData.Leveling(this.uILvUpAbility.ShipAbility.levelUpStrategy);
        UIEvenLvUp.Instance.Toggle();
    }
    protected virtual void CheckAbilityGlobal(AbilityShipAbstract abilityGlobal)
    {
        if (abilityGlobal.ShipPlayAbleAbilities != null) return;
        abilityGlobal.transform.SetParent(PlayerCtrl.instance.CurrentShip.ShipAbilities.transform);
        abilityGlobal.transform.SetPositionAndRotation(PlayerCtrl.instance.CurrentShip.ShipAbilities.transform.position, PlayerCtrl.instance.CurrentShip.ShipAbilities.transform.rotation);
        abilityGlobal.LoadShipPlayAbleAbilities();
        abilityGlobal.gameObject.SetActive(true);
        this.uILvUpAbility.ShipAbility.LoadShipPlayAbleAbilities();
        PlayerCtrl.instance.CurrentShip.ShipAbilities.LoadBaseAbility();
    }
}
