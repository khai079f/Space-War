using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationCenterCtrl : GameMonoBehaviour
{
    [SerializeField] protected ObjectMoveFoward objectMove;
    public ObjectMoveFoward ObjectMove => objectMove;
    [SerializeField] protected ObjectLookAtEnemy objectLook;
    public ObjectLookAtEnemy ObjectLook => objectLook;
    [SerializeField] protected CheckDistanceLevel checkDistanceLevel;
    public CheckDistanceLevel CheckDistanceLevel => checkDistanceLevel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbility(ref this.objectMove, "ObjectMoveFoward");
        this.LoadAbility(ref this.objectLook, "ObjectLookAtEnemy");
        this.LoadAbility(ref this.checkDistanceLevel, "CheckDistanceLevel");
    }
    protected virtual void LoadAbility<T>(ref T component, string componentName) where T : Component
    {
        if (component != null) return;
        component = transform.GetComponentInChildren<T>();
        Debug.LogWarning(transform.name + ": Loaded " + componentName, gameObject);
    }
}
