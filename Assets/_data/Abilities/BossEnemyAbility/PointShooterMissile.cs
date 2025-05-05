using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointShooterMissile : PointShooterWing
{
    public override void UpdateStatuspointShooters(bool status, int count)
    {
        //
    }
    protected override void LoadPointShooters()
    {
        this.pointShooters.Clear();

        foreach (Transform child in transform)
        {
            if (!child.name.Contains("PointShooter")) continue;

            Transform pointShooter = child.GetComponent<Transform>();
            this.pointShooters.Add(pointShooter);
        }

        // Debug.LogWarning($"{transform.name}: Loaded {pointShooters.Count} PointShooters and disabled them", gameObject);
    }
}
