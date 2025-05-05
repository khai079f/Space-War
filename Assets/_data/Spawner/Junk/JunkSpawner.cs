using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawner : Spawner
{
    // Singleton pattern to ensure only one instance of JunkSpawner exists
    protected static JunkSpawner instance;
    public static JunkSpawner Instance { get => instance; }

    protected string meteoriteOne = "Meteorite_1";
    public string MeteoriteOne => meteoriteOne;
    protected string meteoriteTwo = "Meteorite_2";
    public string MeteoriteTwo => meteoriteTwo;

    protected override void Awake()
    {
        base.Awake();
        if (JunkSpawner.instance != null)
        {
            Debug.LogWarning("Only 1 JunkSpawner allowed to exist");
            return; // Dừng lại nếu đã có một instance khác
        }
        JunkSpawner.instance = this;
    }



}
