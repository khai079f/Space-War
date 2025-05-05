using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBomb : AbilityShootbullet
{
    protected override string GetNameAbility()
    {
        return this.abilityBullet = "Bomb";
    }
    protected override string GetNameBullet()
    {
        return this.nameBullet = BulletSpawner.Bomb;

    }
}
