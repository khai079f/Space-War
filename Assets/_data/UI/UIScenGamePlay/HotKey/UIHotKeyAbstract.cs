using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIHotKeyAbstract : GameMonoBehaviour
{
    [SerializeField] protected UIHotKeyCtrl hotKeyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIHotKeyCtrl();
    }
    protected virtual void LoadUIHotKeyCtrl()
    {
        if (this.hotKeyCtrl != null) return;
        this.hotKeyCtrl = GetComponentInParent<UIHotKeyCtrl>();
        Debug.LogWarning(transform.name + ": LoadUIHotKeyCtrl from UI", gameObject);
    }
}

