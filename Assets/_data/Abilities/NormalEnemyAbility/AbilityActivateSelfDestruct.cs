using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActivateSelfDestruct : BaseAbility
{
    [SerializeField] protected AbilityEnemyCtrlAbstract abilityEnemy;
    public AbilityEnemyCtrlAbstract AbilityEnemy => abilityEnemy;
    [SerializeField] protected DameSender dameSender;
    public DameSender DameSender => dameSender;
    private float colorChangeTimer = 0f;
    [SerializeField] protected float DestructTime = 5f;
    private float DestructTimer = 0f;
    private bool isRed = false; 
    private bool isSelfDestruct = false; 

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityEnemyCtrl();
        this.LoadDameSender();
    }
    protected virtual void LoadAbilityEnemyCtrl()
    {
        if (this.abilityEnemy != null) return;
        this.abilityEnemy = transform.GetComponentInParent<AbilityEnemyCtrlAbstract>();
        Debug.LogWarning(transform.name + ":LoadAbilityEnemyCtrl", gameObject);
    }
    protected override void LoadAbilityData()
    {
        if (this.abilityData != null) return;
        string resPath = "ShootableObject/Enemy/Ability/" + transform.name;
        this.abilityData = Resources.Load<AbilityData>(resPath);
        Debug.LogWarning(transform.name + ": LoadAbilityData for Enemy", gameObject);
    }
    protected virtual void LoadDameSender()
    {
        if (this.dameSender != null) return;
        this.dameSender = transform.Find("DameSender").GetComponent<DameSender>();
        Debug.LogWarning(transform.name + ":LoadDameSender", gameObject);
    }
    protected override void OnFixedUpdate()
    {
        if (this.isSelfDestruct) this.DestroyAbility();


    }
    private void ActivateSelfDestruct()
    {
        this.isSelfDestruct = true;
        this.UpdateColor();
    }
    private void UpdateColor( )
    {
        this.colorChangeTimer += Time.deltaTime;
        if (this.colorChangeTimer >= 0.2f)
        {
            this.colorChangeTimer = 0f; // Reset bộ đếm
            this.isRed = !isRed; // Chuyển đổi trạng thái màu
            this.abilityEnemy.BaseEnemyCtrl.enemyModel.Sprite.color = isRed ? Color.red : Color.white;
        }
    }
    public override void UseAbilitySkill()
    {
        this.ActivateSelfDestruct();
       
    }
    protected virtual void DestroyAbility()
    {
        this.DestructTimer += Time.fixedDeltaTime;
        if(this.DestructTimer >= this.DestructTime) this.AbilityEnemy.BaseEnemyCtrl.Despawn.DeSpawnObj(this.AbilityEnemy.BaseEnemyCtrl,this.abilityData);   
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.isSelfDestruct = false;
        this.DestructTimer = 0;
        this.abilityEnemy.BaseEnemyCtrl.enemyModel.Sprite.color= Color.white;
    }

}
