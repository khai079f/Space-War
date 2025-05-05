using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSpawner : Spawner
{
    protected static HPSpawner instance;
    public static HPSpawner Instance => instance;

    public static string HPBar = "HPBar";

    protected override void Awake()
    {
        base.Awake();
        if (HPSpawner.instance != null) Debug.LogWarning("Only 1 HPSpawner allow to exist");
        HPSpawner.instance = this;
    }
}
