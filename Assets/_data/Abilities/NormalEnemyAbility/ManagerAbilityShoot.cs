using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAbilityShoot : GameMonoBehaviour
{
    [SerializeField] protected AbilityShipPlayerShootBooster abilityShipPlayerShoot;
    protected float rightMouseHoldTime = 0f; // Thời gian giữ chuột phải
    protected bool isRightMouseHeld = false;


    private void Update()
    {
        if (UIEvenLvUp.Instance.isPaused) return;
        this.HandleInput();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityShipPlayerShoot();
    }

    protected virtual void LoadAbilityShipPlayerShoot()
    {
        if (this.abilityShipPlayerShoot != null) return;
        this.abilityShipPlayerShoot = GetComponent<AbilityShipPlayerShootBooster>();
        Debug.LogWarning(transform.name + ": LoadAbilityShipPlayerShoot", gameObject);
    }

    protected virtual void HandleInput()
    {
        if (Input.GetMouseButton(0)) // Nhấn giữ chuột trái
        {
            this.BoosterDame(true);
        }
        if (Input.GetMouseButtonUp(0)) // Khi thả chuột trái
        {
            this.BoosterDame(false);
        }

        if (Input.GetMouseButton(1)) // Nhấn giữ chuột phải
        {
            this.HandleRightClick();
        }

    }

    protected virtual void BoosterDame(bool state)
    {
        if (this.abilityShipPlayerShoot != null)
        {
            this.abilityShipPlayerShoot.SetStatusBoosterDame(state); // Kích hoạt bắn từ chuột trái
        }
    }

    protected virtual void HandleRightClick()
    {
        if (this.abilityShipPlayerShoot.GetStateShootAngleOffset())
        {
            this.abilityShipPlayerShoot.SetStateShootAngleOffset(false);
        }
        else
        {
            this.abilityShipPlayerShoot.SetStateShootAngleOffset(true);
        }
    }


}
