using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearBigger : GameMonoBehaviour
{
    [SerializeField] protected float speedScale = 0.01f;
    [SerializeField] protected float currentScale = 0f;
    [SerializeField] protected float startScale = 0.1f;
    [SerializeField] protected float maxScale = 1f;
    [SerializeField] protected bool isAppearing = false;
    [SerializeField] protected bool appeared = false;
    public bool IsAppearing => isAppearing;
    public bool Appeared => appeared;
    public virtual void Appearing(Transform transform)
    {
        this.currentScale += this.speedScale;
        transform.localScale = new Vector3(this.currentScale, this.currentScale, this.currentScale);
        if (this.currentScale >= this.maxScale) this.Appear(transform);
    }

    public virtual void InitScale(Transform transform)
    {
        transform.localScale = Vector3.zero;
        this.currentScale = this.startScale;
    }

    public virtual void Appear(Transform transform)
    {
        this.appeared = true;
        this.isAppearing = false; // Dừng quá trình xuất hiện khi hoàn tất
        transform.localScale = new Vector3(this.maxScale, this.maxScale, this.maxScale);
        
    }
    public virtual void ResetAppeared()
    {
        this.isAppearing = false;
        this.appeared = false;
    }
}
