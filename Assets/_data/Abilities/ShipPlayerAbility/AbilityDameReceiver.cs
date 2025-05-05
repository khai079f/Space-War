using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDameReceiver : DameReceiver
{
    [SerializeField] protected SpriteRenderer sprite;

    protected CircleCollider2D circleCollider
    {
        get { return receiverCollider as CircleCollider2D; }
        set { receiverCollider = value; }
    }

    protected override void Start()
    {
        base.Start();
        this.StatusActive(true);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpriteRenderer();
        EnsureCircleCollider();
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (this.sprite != null) return;
        this.sprite =transform.parent.Find("ModelSprite").GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + ": LoadSpriteRenderer", gameObject);
    }

    protected virtual void EnsureCircleCollider()
    {
        if (receiverCollider != null && receiverCollider is CircleCollider2D)
            return;

        if (receiverCollider != null)
            Destroy(receiverCollider);

        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        Debug.Log(transform.name + ": Converted to CircleCollider2D", gameObject);
    }

    public virtual void UpdateLocalScale(float shipDiameter)
    {
        if (sprite == null || sprite.sprite == null)
        {
            Debug.LogWarning("Sprite or sprite data is null, cannot update scale.");
            return;
        }

        float circleNativeDiameter = sprite.sprite.bounds.size.x;
        float scaleFactor = shipDiameter / circleNativeDiameter;
        sprite.transform.localScale = new Vector3(scaleFactor, scaleFactor, sprite.transform.localScale.z);
    }

    public virtual void UpdateRadiusCollider(float shipDiameter)
    {
        CircleCollider2D col = receiverCollider as CircleCollider2D;
        if (col == null)
        {
            Debug.LogWarning("Collider is not a CircleCollider2D, cannot update radius.");
            return;
        }
        col.radius = shipDiameter / 2f;
        //Debug.Log(transform.name + ": Updated collider radius to " + col.radius, gameObject);
    }
    public virtual void SetStateDead(bool status)
    {
        this.isDead = status;
    }
    public virtual void StatusActive(bool status)
    {
        this.sprite.transform.gameObject.SetActive(status);
        this.transform.gameObject.SetActive(status);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.StatusActive(true);
        this.SetStateDead(false);
    }
    protected override void OnDead()
    {
        this.StatusActive(false);
    }
}
