using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOverdriveStrike : AbilityEnemyAbstract
{
    [SerializeField] protected Transform pointTarget; // Tốc độ lao vào
    [SerializeField] protected DrawSkillIndicator drawSkillIndicator;
    [SerializeField] protected float attackRange = 15f; // Khoảng cách cần di chuyển
    [SerializeField] protected float speedStrike = 10f; // Tốc độ lao vào
    [SerializeField] private Vector3 targetPosition;   // Vị trí mục tiêu
    [SerializeField] private bool isStriking = false;  // Cờ đánh dấu đang lao vào
    [SerializeField] protected bool isChargeEnergy = false;
    [SerializeField] protected float timeChargeEnergy = 3;
    protected float timerChargeEnergy =0;
    public bool IsChargeEnergy => isChargeEnergy;
    protected string abilityOverdriveStrike;
    protected AbilityCanCtrlModel ctrlModel;

    protected override void Start()
    {
        base.Start();
        this.pointTarget.position = new Vector3(transform.position.x, transform.position.y - this.attackRange, transform.position.z);
        this.ctrlModel = new AbilityCanCtrlModel();
        this.ctrlModel.SaveOriginalValues(abilityEnemyCtrl.BaseEnemyCtrl.enemyModel.ParticleFires);
        // Update UpdateRange
        this.UpdateSkillIndicator();


    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPointTarget();
        this.LoadDrawSkillIndicator();
    }
    protected override void LoadAbilityData()
    {
        if (this.abilityData != null) return;
        string resPath = "ShootableObject/Enemy/Ability/" + this.GetNameAbility();
        this.abilityData = Resources.Load<AbilityData>(resPath);
        Debug.LogWarning(transform.name + ": LoadAbilityData for Enemy", gameObject);
    }
    protected virtual void LoadPointTarget()
    {
        if (this.pointTarget != null) return;
        this.pointTarget = transform.Find("PointTarget").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadPointTarget", gameObject);
    }
    protected virtual void LoadDrawSkillIndicator()
    {
        if (this.drawSkillIndicator != null) return;
        this.drawSkillIndicator = transform.Find("SkillIndicator").GetComponent<DrawSkillIndicator>();
        Debug.LogWarning(transform.name + ": LoadDrawSkillIndicator", gameObject);
    }
    protected virtual string GetNameAbility()
    {
        return this.abilityOverdriveStrike = "OverdriveStrike";
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        this.OverdriveStrike();
    }

    protected virtual void OverdriveStrike()
    {
        if (!isReady) return;
        if(!isStriking) this.SetTargetPosition();
        if (!ChargeEnergy(this.timeChargeEnergy)) return;
        // Bắt đầu kích hoạt kỹ năng
        this.isStriking = true;
        MoveToTarget();
    }

    protected virtual Vector3 SetTargetPosition()
    {
        return targetPosition = this.pointTarget.position;
    }
    private void MoveToTarget()
    {
        this.ctrlModel.ChangeColorParticle(abilityEnemyCtrl.BaseEnemyCtrl.enemyModel.ParticleFires, default, 1);
        if (Vector3.Distance(abilityEnemyCtrl.BaseEnemyCtrl.transform.position, targetPosition) > 0.5f)
        {
            Debug.Log(Vector3.Distance(abilityEnemyCtrl.BaseEnemyCtrl.transform.position, targetPosition));
            // Di chuyển từ từ đến targetPosition
            abilityEnemyCtrl.BaseEnemyCtrl.transform.position = Vector3.MoveTowards(
                abilityEnemyCtrl.BaseEnemyCtrl.transform.position,
                targetPosition,
                speedStrike * Time.fixedDeltaTime
            );

        }
        else
        {
            this.isStriking = false; // Đặt lại cờ đánh dấu
            this.Active();

        }

    }
    public override void Active()
    {
        base.Active();
        this.isChargeEnergy = false;
        this.ctrlModel.RestoreOriginalValues();
    }
    protected virtual bool ChargeEnergy(float timeChargeEnergy)
    {
        if (this.isChargeEnergy) return isChargeEnergy;
        if (this.timerChargeEnergy >= timeChargeEnergy)
        {
            this.isChargeEnergy = true;
            this.timerChargeEnergy = 0;
            this.drawSkillIndicator.StateDrawSkillIndicator(false);
        }
        else
        {
            this.timerChargeEnergy += Time.fixedDeltaTime;
            this.ctrlModel.ChangeColorParticle(abilityEnemyCtrl.BaseEnemyCtrl.enemyModel.ParticleFires,default,2);
            this.drawSkillIndicator.StateDrawSkillIndicator(true);
            this.drawSkillIndicator.ChangeHeightByTime(this.timerChargeEnergy, timeChargeEnergy);
        }
        return isChargeEnergy;
    }
    protected virtual void UpdateSkillIndicator()
    {
        this.drawSkillIndicator.UpdateRange(this.attackRange,new Vector3(2.5f,1,1));
    }
}
