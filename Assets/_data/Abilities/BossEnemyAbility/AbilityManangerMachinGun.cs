using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManangerMachinGun : GameMonoBehaviour
{
    [SerializeField] protected List<AbilityNormalShoot> abilityNormalShoots = new List<AbilityNormalShoot>();
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
        if (this.abilityNormalShoots.Count != 0) this.abilityNormalShoots.Clear();
        AbilityNormalShoot[] normalShoots = transform.GetComponentsInChildren<AbilityNormalShoot>();
        if (normalShoots.Length == 0) return;
        this.abilityNormalShoots.AddRange(normalShoots);
    }
    protected virtual void LoadAbilitySilverlightEnemy()
    {
        if (this.abilitySilverlightEnemy != null) return;
        this.abilitySilverlightEnemy = transform.GetComponentInParent<AbilitySilverlightEnemy>();
        Debug.LogWarning(transform.name+ " LoadAbilitySilverlightEnemy",gameObject);
    }
    public virtual void AllGunFire()
    {
        foreach(AbilityNormalShoot abilityNormalShoot in abilityNormalShoots)
        {
            abilityNormalShoot.Shoot();
        }
    }
}
