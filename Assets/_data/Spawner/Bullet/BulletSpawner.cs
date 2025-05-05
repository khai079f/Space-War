using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    protected static BulletSpawner instance;
    public static BulletSpawner Instance { get => instance; }

    public static string bulletOne = "Bullet_1";
    public static string bulletTwo = "Bullet_2";
    public static string bulletThree = "Bullet_3";
    public static string bulletRoket = "BulletRoket";
    public static string bulletMissile = "Missile_1";
    public static string Bomb = "Bomb";

    protected override void Awake()
    {
        base.Awake();
        if (BulletSpawner.instance != null) Debug.LogWarning("Only 1 BulletSpawner allow to exist");
        BulletSpawner.instance = this;
    }


}
