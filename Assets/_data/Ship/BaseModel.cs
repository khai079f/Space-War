using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModel : GameMonoBehaviour
{
    [Header("BaseModel")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected float shipDiameter ;
    protected override void Start()
    {
        base.Start();
        this.UpdateShipDiameter();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpriteRenderer();
        this.UpdateShipDiameter();
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (this.spriteRenderer != null) return;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        
        Debug.Log(transform.name + ": LoadSpriteRenderer", gameObject);
    }
    protected virtual void UpdateShipDiameter()
    {
        if (spriteRenderer != null )
        {
            Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
            this.shipDiameter = Mathf.Sqrt(spriteSize.x * spriteSize.x + spriteSize.y * spriteSize.y);
        }
    }
    public virtual float GetShipDiameter()
    {
        return this.shipDiameter;
    }
}
