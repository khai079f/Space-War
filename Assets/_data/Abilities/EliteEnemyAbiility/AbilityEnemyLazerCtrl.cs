using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnemyLazerCtrl : AbilityEnemyAbstract
{
    [Header("Ability Enemy Lazer Ctrl")]
    [SerializeField] protected Transform shootPoint;
    public Transform ShootPoint => shootPoint;
    [SerializeField] protected LazerDameSender lazerDameSender;
    public LazerDameSender LazerDameSender => lazerDameSender;
    [SerializeField] protected ModelLazerCtrl modelLazerCtrl;
    public ModelLazerCtrl ModelLazerCtrl => modelLazerCtrl;
    [SerializeField] protected LazerEnemyShootImpact lazerImpact;
    [SerializeField] protected DrawSkillIndicator drawSkillIndicator; 
    protected Quaternion rotation;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShootPoint();
        this.LoadAbility(ref this.drawSkillIndicator, "DrawSkillIndicator");
        this.LoadAbility(ref this.modelLazerCtrl, "ModelLazerCtrl");
        this.LoadAbility(ref this.lazerDameSender, "LazerDameSender");
        this.LoadAbility(ref this.lazerImpact, "LazerEnemyShootImpact");
    }
    protected virtual void LoadShootPoint()
    {
        if (this.shootPoint != null) return;
        this.shootPoint = transform.Find("LazerShoot").Find("ShootPoint").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ":LoadShootPoint for lazer", gameObject);
    }


    protected virtual void LoadAbility<T>(ref T component, string componentName) where T : Component
    {
        if (component != null) return;
        component = transform.GetComponentInChildren<T>();
        Debug.LogWarning(transform.name + ": Loaded " + componentName, gameObject);
    }

    protected override void LoadAbilityData()
    {
        if (this.abilityData != null) return;
        string resPath = "ShootableObject/Enemy/Ability/" + transform.name;
        this.abilityData = Resources.Load<AbilityData>(resPath);
        Debug.LogWarning(transform.name + ": LoadAbilityData for Enemy", gameObject);
    }
    public virtual Transform UpdateShootPoint()
    {
        if (this.shootPoint == null) return null;
        return this.shootPoint;
    }
}
