using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : BaseUI
{
    [SerializeField] protected VolumeUICtrl volumeUI;
    public VolumeUICtrl VolumeUI=>volumeUI;
    private static UISetting instance;
    public static UISetting Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (UISetting.instance != null) Debug.LogError("Only 1 UIInventory allow to exist");
        UISetting.instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadVolumeUICtrl();
    }
    protected virtual void LoadVolumeUICtrl()
    {
        if (this.volumeUI != null) return;
        this.volumeUI = transform.Find("VolumeUI").GetComponent<VolumeUICtrl>();
        Debug.Log(transform.name + " LoadVolumeUICtrl", gameObject);
    }


}
