using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLazerCtrl : AbilitylethalityShipPlayer
{
    [Header("Ability Lazer Ctrl")]
    [SerializeField] protected ModelLazerCtrl modelLazerCtrl;
    public ModelLazerCtrl ModelLazerCtrl => modelLazerCtrl;
    [SerializeField] protected Transform shootPoint;
    public Transform ShootPoint => shootPoint;
    [SerializeField] protected LazerDameSender lazerDameSender;
    public LazerDameSender LazerDameSender => lazerDameSender;
    [SerializeField] protected LazerPlayerShootImpact lazerImpact;
    [SerializeField] protected IntrinsicPrecisionBeam intrinsic;

    public IntrinsicPrecisionBeam Intrinsic => intrinsic;
    protected Quaternion rotation;
    protected override void Start()
    {
        base.Start();
        LoadIntrinsic();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShootPoint();
        this.LoadAbility(ref this.modelLazerCtrl, "ModelLazerCtrl");
        this.LoadAbility(ref this.lazerDameSender, "LazerDameSender");
        this.LoadAbility(ref this.lazerImpact, "LazerImpact");
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

    protected virtual void LoadIntrinsic()
    {
        if (this.shipPlayAbleAbilities.BaseShipAbilitys == null) return;
        foreach (AbilityShipAbstract intrinsic in this.shipPlayAbleAbilities.BaseShipAbilitys)
        {
            this.intrinsic = intrinsic as IntrinsicPrecisionBeam;
            if (this.intrinsic != null) return;
        }
    }
    public void UpdateLaserVisualsByLevel()
    {
        int level = abilityData.Lv; // Ví dụ, lấy cấp từ abilityData
        modelLazerCtrl.UpdateLaserColorByLevel(level);

        // Ngoài ra, nếu muốn cập nhật LaserWidth theo cấp:
        float baseWidth = 0.2f;
        float newWidth = baseWidth + level * 0.05f;
        modelLazerCtrl.UpdateLaserWidth(newWidth);
    }


    public override void WhatlevelUpStrategy()
    {
        this.levelUpStrategy = new LaserLevelUp(this);
    }
    protected bool HasDameReceiver(RaycastHit2D hit) => hit.collider.GetComponent<DameReceiver>() != null;

    protected bool ShouldIgnoreCollisionWithSelf(Collider2D other) => other.transform.parent == transform.parent.parent;

    protected override void OnEnable()
    {
        base.OnEnable();
        abilityData.OnLevelUp += UpdateLaserVisualsByLevel;
        this.UpdateLaserVisualsByLevel();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        abilityData.OnLevelUp -= UpdateLaserVisualsByLevel;

    }
}