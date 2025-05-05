using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShipImpact : ImpactAbstract
{
    [SerializeField] protected PolygonCollider2D shippolygonCollider;
    [SerializeField] protected BodyDameSender bodyDameSender;
    protected override string GetTypeShooter()
    {
        if(this.baseShooter == null) this.baseShooter = transform.GetComponentInParent<BaseShipCtrl>();
        return this.shooterType = this.baseShooter.WhatType();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBodyDameSender();
    }
    protected override void LoadCollider()
    {
        if (this.shippolygonCollider != null) return;
        this.shippolygonCollider = GetComponent<PolygonCollider2D>();
        Debug.LogWarning(transform.name + " LoadCollider", gameObject);
    }
    protected override void LoadCtrlForShooter()
    {
        if (this.baseShooter != null) return;
        this.baseShooter = transform.GetComponentInParent<BaseShipCtrl>();
        Debug.LogWarning(transform.name + " LoadCtrlForShooter", gameObject);
    }
    protected virtual void LoadBodyDameSender()
    {
        if (this.bodyDameSender != null) return;
        this.bodyDameSender = transform.GetComponent<BodyDameSender>();
        Debug.LogWarning(transform.name + " LoadBodyDameSender", gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.CheckTypeShooter(other)) return;
        if (!this.CheckColliderShip(other)) return;
        this.bodyDameSender.Send(other.transform,other);
    }
}
