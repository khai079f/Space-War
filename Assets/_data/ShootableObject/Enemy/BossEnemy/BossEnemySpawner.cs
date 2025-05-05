using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawner : Spawner
{

    protected static BossEnemySpawner instance;
    public static BossEnemySpawner Instance => instance;


    protected override void Awake()
    {
        base.Awake();
        if (BossEnemySpawner.instance != null)
        {
            Debug.LogWarning("Only 1 BossEnemySpawner allowed to exist");
            return; // Dừng lại nếu đã có một instance khác
        }
        BossEnemySpawner.instance = this;
    }



}