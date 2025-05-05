using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DameReceiver : GameMonoBehaviour
{
    [SerializeField] protected Collider2D receiverCollider;
    public Collider2D ShipCollider => receiverCollider;
    [SerializeField] protected float  hp = 1;
    [SerializeField] protected float maxHp = 2;

    [SerializeField] protected bool isDead = false;
    protected float sphereColliderRadius = 0.3f;
    public event System.Action OnDeath;
    protected override void OnEnable()
    {
        this.Reborn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.Reborn();

    }

    protected virtual void LoadCollider()
    {
        if (this.receiverCollider != null) return;
        this.receiverCollider = GetComponent<Collider2D>();
        this.receiverCollider.isTrigger = true;
        //Debug.Log(transform.name + ": Collider", gameObject);
    }
    protected virtual void Reborn()
    {
        this.hp = this.maxHp;
        this.isDead = false;
    }

    protected virtual void Add(int add)
    {
        this.hp += add;
        if (this.hp > this.maxHp) this.hp = this.maxHp;
    }

    public virtual void Deduct(float deduct)
    {
        if (this.isDead) return;

        this.hp -= deduct;
        if (this.hp < 0) this.hp = 0;
        this.CheckIsDead();
    }

    public virtual bool IsDead()
    {
        return this.hp <= 0;
    }

    protected virtual void CheckIsDead()
    {
        if (this.isDead) return; // Only check if not already dead

        if (this.IsDead())
        {
            this.isDead = true;
            this.OnDead();
            this.OnDeath?.Invoke(); // Trigger event
        }
    }
    public virtual float GetCurrentHp()
    {
        if (this.IsDead()) return 0;
        return this.hp;
    }
    public virtual float GetMaxHp()
    {
        return this.maxHp;
    }
    public virtual void IncreaseCurrentHp(float value )
    {
        this.hp = this.hp + value;
    }
    public virtual void IncreaseCurrentMaxHp(float value)
    {
        this.maxHp = this.maxHp + value;
    }
    public virtual void SetCurrentHp(float value)
    {
        this.hp =  value;
    }
    public virtual void SetCurrentMaxHp(float value)
    {
        this.maxHp =  value;
    }
    protected abstract void OnDead();

}
