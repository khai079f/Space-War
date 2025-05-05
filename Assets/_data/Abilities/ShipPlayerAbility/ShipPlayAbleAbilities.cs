using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipPlayAbleAbilities : GameMonoBehaviour
{
    [Header("Abilities")]
    [SerializeField] protected ShipCtrl shipCtrl;
    public ShipCtrl ShipCtrl => shipCtrl;
    [SerializeField] protected List<AbilityShipAbstract>  baseShipAbilitys;
    public List<AbilityShipAbstract> BaseShipAbilitys => baseShipAbilitys;
    [SerializeField] protected float maxMana = 0;
    [SerializeField] protected float currentMana = 0;
    [SerializeField] private float manaDeductionTimer = 0f; // Biến theo dõi thời gian trừ mana
    public event Action<List<AbilityShipAbstract>> OnBaseAbilitiesUpdated;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipCtrl();
        this.LoadBaseAbility();
    }

    protected virtual void LoadShipCtrl()
    {
        if (this.shipCtrl != null) return;
        this.shipCtrl = transform.GetComponentInParent<ShipCtrl>();
        Debug.LogWarning(transform.name + ": LoadShipCtrl", gameObject);
    }
    public virtual void LoadBaseAbility()
    {
        if (this.baseShipAbilitys.Count > 0) this.baseShipAbilitys.Clear();
        this.baseShipAbilitys.AddRange(GetComponentsInChildren<AbilityShipAbstract>());
        //Debug.LogWarning(transform.name + ": LoadBaseAbility", gameObject);
        this.OnBaseAbilitiesUpdated?.Invoke(this.baseShipAbilitys);
    }
    protected override void Start()
    {
        base.Start();
        this.Reborn();
        StartCoroutine(ManaRecoveryRoutine());
    }

    protected virtual IEnumerator ManaRecoveryRoutine()
    {

        while (true)
        {
            float time = 0.05f;
            Recove(this.ShipCtrl.ShipProfileSO.ManaRecovery * time); // Hồi mana

            yield return new WaitForSeconds(time); // Đợi 1 giây
        }
    }

    protected virtual void Reborn()
    {
        this.maxMana = this.ShipCtrl.ShipProfileSO.Mana;
        this.currentMana = this.maxMana;
    }

    protected virtual void Recove(float ManaRecovery)
    {
        this.currentMana += ManaRecovery;
        if (this.currentMana > this.maxMana) this.currentMana = this.maxMana;
    }

    public virtual bool DeductMana(float deduct)
    {
        if (this.currentMana < deduct) return false;
        this.currentMana -= deduct;
        return true;
    }
    public virtual float DeductMana(float totalDeduct, float time)
    {
        if (this.currentMana < totalDeduct) return 0; // Không đủ mana, thoát
        if (time <= 0) // Thời gian <= 0, trừ ngay lập tức
        {
            this.currentMana -= totalDeduct;
            return totalDeduct;
        }

        // Tính toán lượng mana cần trừ trong khung hình hiện tại
        float manaPerFrame = totalDeduct / time * Time.deltaTime;
        // Tích lũy thời gian trừ mana
        this.manaDeductionTimer += Time.deltaTime;

        // Nếu thời gian chưa đủ, trừ dần
        if (this.manaDeductionTimer < time)
        {
            this.currentMana -= manaPerFrame;
            if (this.currentMana < 0) this.currentMana = 0; // Đảm bảo không bị âm
        }
        else
        {
            // Reset sau khi trừ đủ
          //  this.currentMana -= totalDeduct - (manaPerFrame * (this.manaDeductionTimer - time));
            this.manaDeductionTimer = 0f;
        }
        return manaPerFrame;
    }
    public virtual void UpdateCurrentMana(float value)
    {
        this.currentMana += value;
    }
    public virtual void UpdateMaxMana(float value)
    {
        this.maxMana += value;
    }
    public virtual float GetCurrentMana()
    {
        return this.currentMana;
    }
    public virtual float GetMaxMana()
    {
        return this.maxMana;
    }
}
