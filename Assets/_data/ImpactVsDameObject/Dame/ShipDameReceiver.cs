using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDameReceiver : ShipObjDameReceiver
{
    [SerializeField] protected float force =10f;
    [SerializeField] protected float duration=1f;

    public override void Deduct(float deduct)
    {
        AbilityShipEnergyShield shield = CheckShield();
        float damededuct = deduct;
        if (shield != null) damededuct = shield.ShieldTakeDamage(deduct);
        base.Deduct(this.ReduceDameByArmor(damededuct));
    }
    protected virtual float ReduceDameByArmor(float damededuct)
    {
        return damededuct - (damededuct * this.armor / 100);
    }
    protected virtual AbilityShipEnergyShield CheckShield()
    {
        foreach (var ability in this.shipCtrl.ShipAbilities.BaseShipAbilitys)
        {
            if (ability is AbilityShipEnergyShield shield)
            {
                return shield; // Trả về AbilityShipEnergyShield đầu tiên được tìm thấy
            }
        }
        return null; // Nếu không tìm thấy, trả về null
    }

    public virtual void ApplyKnockback(Collider2D collider)
    {
        Vector2 knockbackDirection = new Vector2(-1f, 0f).normalized; // Hướng mặc định
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(KnockbackRoutine(knockbackDirection));
        }

       
    }

    private IEnumerator KnockbackRoutine(Vector2 direction)
    {
        float elapsed = 0f;

        while (elapsed < this.duration)
        {
            elapsed += Time.deltaTime;

            // Giảm dần lực văng theo thời gian
            float stepForce = this.force * (1 - (elapsed / this.duration));

            // Di chuyển đối tượng theo hướng văng
            transform.parent.position += (Vector3)(direction * stepForce * Time.deltaTime);

            yield return null; // Đợi 1 frame
        }
    }


}

