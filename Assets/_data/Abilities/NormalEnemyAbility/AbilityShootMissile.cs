using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShootMissile : AbilityShootbullet
{
    protected override string GetNameAbility()
    {
        return this.abilityBullet = "HawkbladeMissile";
    }
    protected override string GetNameBullet()
    {
        return this.nameBullet = BulletSpawner.bulletMissile;

    }
}
