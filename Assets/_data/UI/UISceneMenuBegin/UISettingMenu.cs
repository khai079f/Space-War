using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettingMenu : BaseLoadUI
{
    [SerializeField] protected UISetting setting;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUISetting();

    }
    protected virtual void LoadUISetting()
    {
        if (setting != null) return;
        this.setting = transform.Find("UISetting").GetComponent<UISetting>();
        this.setting.gameObject.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadUISetting from UI Center", gameObject);
        if (setting == null) Debug.LogWarning(transform.name + ": LoadUISetting from UI Center failed, Please check Active has states true", gameObject);
    }

    public override void RegisterStartUI()
    {
        this.ToggleUIForInitialization(this.setting.gameObject);

    }

    protected override void Start()
    {
        base.Start();
        this.RegisterStartUI();
    }
}