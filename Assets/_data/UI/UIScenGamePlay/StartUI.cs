using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : GameMonoBehaviour
{
    [SerializeField] protected UICenterCtrl UICenterCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUICtrl();
    }

    protected virtual void LoadUICtrl()
    {
        if (UICenterCtrl != null) return;
        this.UICenterCtrl =transform.Find("UICenter").GetComponent<UICenterCtrl>();
        Debug.LogWarning(transform.name + ": LoadUICtrl", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.UICenterCtrl.RegisterStartUI();
    }


}
