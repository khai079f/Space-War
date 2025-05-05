using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoosterDameSender : BulletDameSender
{
    protected override void SetValueForBullet()
    {
        this.dame = this.bulletCtrl.AbilityData.Dame * 2;
    }
}
