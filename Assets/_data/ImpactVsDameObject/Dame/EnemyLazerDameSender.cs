using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazerDameSender : LazerDameSender
{
    protected override void Start()
    {
        base.Start();
        this.SetValueForLazer();
    }
    protected override void SetValueForLazer(float time =0)
    {
        this.dame = this.baseAbility.AbilityData.Dame;
    }
}
