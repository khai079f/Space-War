using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemySpawner : Spawner
{
    // Singleton pattern to ensure only one instance of JunkSpawner exists
    protected static EliteEnemySpawner instance;
    public static EliteEnemySpawner Instance => instance; 


    protected override void Awake()
    {
        base.Awake();
        if (EliteEnemySpawner.instance != null)
        {
            Debug.LogWarning("Only 1 MotherShipSpawner allowed to exist");
            return; // Dừng lại nếu đã có một instance khác
        }
        EliteEnemySpawner.instance = this;
    }



}