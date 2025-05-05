using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : GameMonoBehaviour
{
    [Header("Base Ability")]
    [SerializeField] protected AbilityData abilityData;
    public AbilityData AbilityData => abilityData;
    [SerializeField] protected float timer = 0;
    [SerializeField] protected float delay ;
    [SerializeField] protected bool isReady = false;
     public bool IsReady => isReady;
    [SerializeField] public bool isChoose = false;


    protected override void Start()
    {
        base.Start();
        this.delay = this.abilityData.Cooldown;

    }
    protected override void ResetValue()
    {
        this.delay = this.abilityData.Cooldown;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityData();
    }
    private void FixedUpdate()
    {
        if (IsPaused()) return;
        this.OnFixedUpdate();
    }
    protected virtual void OnFixedUpdate()
    {
        if (UIEvenLvUp.Instance.isPaused) return;
        
        this.Timing();
    }
    private void Update()
    {
        if (IsPaused()) return;
        this.OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        //
    }
    protected virtual void Timing()
    {
        
        if (this.isReady) return;
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.delay) return;
        this.isReady = true;
      
    }

    public virtual void Active()
    {
        this.isReady = false;
        this.timer = 0;
    }
    public virtual float GetTimer()
    {
       return this.timer;
    }
    public bool IsAbilityReady() // Phương thức kiểm tra khả năng
    {
        return this.isReady; // Trả về trạng thái khả năng
    }
    // Kiểm tra trạng thái Paused
    protected bool IsPaused()
    {
        return Time.timeScale == 0f;
    }
    protected abstract void LoadAbilityData();
    public virtual void UseAbilitySkill() { }
    public virtual int GetValueStackForUI() { return 0; }
}
