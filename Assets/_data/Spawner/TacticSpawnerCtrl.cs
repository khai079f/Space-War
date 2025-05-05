using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticSpawnerCtrl : Spawner
{
    protected static TacticSpawnerCtrl instance;
    public static TacticSpawnerCtrl Instance => instance;


    protected override void Awake()
    {
        base.Awake();
        if (TacticSpawnerCtrl.instance != null)
        {
            Debug.LogWarning("Only 1 TacticSpawnerCtrl allowed to exist");
            return; // Dừng lại nếu đã có một instance khác
        }
        TacticSpawnerCtrl.instance = this;
    }



}
