using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMissileMananger : GameMonoBehaviour
{
    [SerializeField] protected List<AbilityMissile> abilityMissiles = new List<AbilityMissile>();
    [SerializeField] protected AbilitySilverlightEnemy abilitySilverlightEnemy;
    public AbilitySilverlightEnemy abilitySilverlight => abilitySilverlightEnemy;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityNormalShoot();
        this.LoadAbilitySilverlightEnemy();
    }
    protected virtual void LoadAbilityNormalShoot()
    {
        if (this.abilityMissiles.Count != 0) this.abilityMissiles.Clear();
        AbilityMissile[] missilesShoots = transform.GetComponentsInChildren<AbilityMissile>();
        if (missilesShoots.Length == 0) return;
        this.abilityMissiles.AddRange(missilesShoots);
    }
    protected virtual void LoadAbilitySilverlightEnemy()
    {
        if (this.abilitySilverlightEnemy != null) return;
        this.abilitySilverlightEnemy = transform.GetComponentInParent<AbilitySilverlightEnemy>();
        Debug.LogWarning(transform.name + " LoadAbilitySilverlightEnemy", gameObject);
    }
    public virtual void AllGunFire()
    {
        foreach (AbilityMissile abilityMissile in abilityMissiles)
        {
            abilityMissile.Shoot();
        }
    }
}
