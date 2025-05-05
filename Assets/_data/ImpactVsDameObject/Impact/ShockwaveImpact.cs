using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveImpact : ImpactAbstract
{
    [SerializeField] protected CircleCollider2D circleCollider;
    [SerializeField] protected ShockwaveCtrl shockwaveCtrl;
    [SerializeField] protected float maxRadius = 5f; // Bán kính tối đa
    [SerializeField] protected float expandDuration = 1f; // Thời gian giãn nở
    [SerializeField] protected float speedMultiplier = 3f; // Điều chỉnh tốc độ mở rộng
    protected override void LoadCtrlForShooter()
    {
        if (shockwaveCtrl != null) return;
        this.shockwaveCtrl = transform.GetComponentInParent<ShockwaveCtrl>();
        Debug.Log(transform.name + ": ShockwaveCtrl", gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.CheckShooter(other)) return;
        if (this.CheckTypeShooter(other)) return;
        this.shockwaveCtrl.DameSender.Send(other.transform);

    }
    protected virtual bool CheckShooter(Collider2D other)
    {
        if (other == null || other.transform == null) return false;

        Transform currentTransform = other.transform;

        if (currentTransform.parent == null) return false;
        if (this.shockwaveCtrl == null || this.shockwaveCtrl.Shooter == null) return false;

        return currentTransform.parent == this.shockwaveCtrl.Shooter.transform;
    }

    protected override string GetTypeShooter()
    {
        if (this.shockwaveCtrl.Shooter == null) return null;
        this.baseShooter = this.shockwaveCtrl.Shooter;
        return this.shooterType = this.baseShooter.WhatType();
    }

    protected override void LoadCollider()
    {
        if (this.circleCollider != null) return;
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.circleCollider.isTrigger = true;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.ExpandShockwave();
    }
    public void ExpandShockwave()
    {
        StartCoroutine(ExpandColliderRadius());
    }

    private IEnumerator ExpandColliderRadius()
    {
        float startRadius = circleCollider.radius;
        float elapsedTime = 0f;


        while (elapsedTime < expandDuration)
        {
            elapsedTime += Time.deltaTime * speedMultiplier;
            float progress = elapsedTime / expandDuration;

            // Sử dụng Ease-Out để tăng nhanh lúc đầu rồi chậm dần
            float easedProgress = 1f - Mathf.Pow(1f - progress, 3);

            circleCollider.radius = Mathf.LerpUnclamped(startRadius, maxRadius, easedProgress);
            yield return null;
        }

        circleCollider.radius = maxRadius; // Đảm bảo đạt giá trị tối đa
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        circleCollider.radius = 0.5f;
    }
}
