using UnityEngine;
using UnityEngine.UI;

public class HPBar : GameMonoBehaviour
{
    [Header("HP Bar")]
    [SerializeField] protected BaseEnemyCtrl  shootableObjCtrl;
    [SerializeField] protected SliderStatus sliderHP;
    [SerializeField] protected FollowTarget followTarget;
    [SerializeField] protected Spawner spawner;

    protected virtual void FixedUpdate()
    {
        this.HPShowing();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSliderHP();
        this.LoadFollowTarget();
        this.LoadSpawner();
    }
    protected virtual void LoadSliderHP()
    {
        if (this.sliderHP != null) return;
        this.sliderHP = transform.GetComponentInChildren<SliderStatus>();
        Debug.LogWarning(transform.name + ": LoadSlider", gameObject);
    }
    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponentInParent<Spawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }
    protected virtual void LoadFollowTarget()
    {
        if (this.followTarget != null) return;
        this.followTarget = transform.GetComponent<FollowTarget>();
        Debug.LogWarning(transform.name + ": LoadFollowTarget", gameObject);
    }
    public virtual void SetObjCtrl(BaseEnemyCtrl shootableObjCtrl)
    {
        this.shootableObjCtrl = shootableObjCtrl;
    }

    public virtual void SetFollowTarget(Transform followTarget)
    {
        this.followTarget.SetTarget(followTarget);
    }
    protected virtual void HPShowing()
    {

        if (this.shootableObjCtrl == null) return;
        bool isDead = this.shootableObjCtrl.DameReceiver.IsDead();
        if (isDead)
        {
            this.spawner.Despawn(transform);
            return;
        }
        float hp = this.shootableObjCtrl.DameReceiver.GetCurrentHp();
        float maxHp = this.shootableObjCtrl.DameReceiver.GetMaxHp();

        this.sliderHP.UpdateCurrentValue(hp);
        this.sliderHP.UpdateMaxValue(maxHp);
    }



}