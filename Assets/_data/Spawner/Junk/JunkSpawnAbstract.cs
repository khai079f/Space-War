using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawnAbstract : GameMonoBehaviour
{
    [SerializeField] protected JunkSpawnCtrl junkSpawnCtrl;
    public JunkSpawnCtrl JunkSpawnCtrl { get => junkSpawnCtrl; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadJunkSpawnCtrl();
    }

    protected virtual void LoadJunkSpawnCtrl()
    {
        if (this.junkSpawnCtrl != null) return;
        this.junkSpawnCtrl = GetComponent<JunkSpawnCtrl>();
        Debug.Log(transform.name + ": LoadSpawnPoints ", gameObject);
    }
}
