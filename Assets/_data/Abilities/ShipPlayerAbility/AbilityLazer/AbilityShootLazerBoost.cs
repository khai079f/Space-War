using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShootLazerBoost : AbilityShootLazer
{
    [Header("Ability ShootLazer Boost")]
    [SerializeField] private float lazerWidthBoost = 1f;
    [SerializeField] private float StartPointSizeBoost = 0.5f;
    [SerializeField] private float fadeDuration = 0.5f; // Thời gian laser mờ dần
    [SerializeField] private float maxLaserDistance = 50f;

    [SerializeField] protected bool isBoosting = false;
    public bool IsBoosting => isBoosting;
    private float originalWidth = 0f;
    private float timerLazerBoost = 0f;
    private float manaUsedToBoost = 0f;
    private bool isShootingLazerBoost = false;
    public bool IsShootingLazerBoost => isShootingLazerBoost;
    private bool isMousePressed = false;
    private bool hasUpdatedStartSize = false;

    protected override void OnUpdate()
    {
        base.OnUpdate();
        this.ShootingNormalLazer();

        if (!this.isShootingLazerBoost) return;

        this.UpdateLazerBoost(this.lazerWidthBoost);
    }

    private void ShootingNormalLazer()
    {
        if (isShootingLazerBoost) return;

        if (Input.GetMouseButtonDown(1))
        {
            if (!this.isReady) return;
            isMousePressed = true;
            this.ModelLazerCtrl.SetLazerState(this.lazerImpact, true, true, true); // Bắt đầu laser với hiệu ứng đầy đủ
        }

        if (Input.GetMouseButton(1) && isMousePressed)
        {
            this.UpdateStartParticles();
            this.BoostDame(this.shipPlayAbleAbilities.DeductMana(this.abilityData.ManaUse, 1f));

            if (this.shipPlayAbleAbilities.GetCurrentMana() <= this.abilityData.ManaUse)
            {
                StopLazerBoost();
            }
        }

        if (Input.GetMouseButtonUp(1) && isMousePressed)
        {
            StopLazerBoost();
        }
    }

    private void StopLazerBoost()
    {
        isMousePressed = false;
        this.originalWidth = this.ModelLazerCtrl.LineRenderer.startWidth;
        this.isShootingLazerBoost = true;
        this.isBoosting = false;
    }

    private void UpdateStartParticles()
    {
        ModifyStartParticles(this.StartPointSizeBoost);
    }

    private void OriginalStartParticles()
    {
        ModifyStartParticles(-this.StartPointSizeBoost);
    }

    private void ModifyStartParticles(float sizeBoost)
    {
        if (sizeBoost > 0 && hasUpdatedStartSize) return; // Nếu đã cập nhật, không làm gì thêm

        foreach (var particle in this.ModelLazerCtrl.StartParticles)
        {
            if (particle.name == "Beam")
            {
                var mainModule = particle.main;
                var startSizeCurve = mainModule.startSize;

                startSizeCurve.constantMin += sizeBoost;
                startSizeCurve.constantMax += sizeBoost;

                mainModule.startSize = startSizeCurve;
            }
        }

        hasUpdatedStartSize = sizeBoost > 0; // Đặt cờ dựa trên việc tăng hay giảm
    }

    private void UpdateLazerBoost(float lazerWidthBoost)
    {
        if (this.ModelLazerCtrl == null || this.ModelLazerCtrl.LineRenderer == null) return;

        this.ModelLazerCtrl.LineRenderer.startWidth = lazerWidthBoost;
        this.ModelLazerCtrl.LineRenderer.endWidth = lazerWidthBoost;

        if (this.shootPoint == null) return;

        this.ModelLazerCtrl.LineRenderer.SetPosition(0, shootPoint.position);
        Vector2 direction = shootPoint.up * -1;
        
        if (this.ModelLazerCtrl.StartVFX != null)
        {
            this.ModelLazerCtrl.StartVFX.transform.position = shootPoint.position;
        }

        Vector2 endPosition = (Vector2)shootPoint.position + direction.normalized * maxLaserDistance;
        this.ModelLazerCtrl.LineRenderer.SetPosition(1, endPosition);
        
        if (this.ModelLazerCtrl.EndVFX != null)
        {
            this.ModelLazerCtrl.EndVFX.transform.position = this.ModelLazerCtrl.LineRenderer.GetPosition(1);
        }
        
        if (this.lazerImpact != null)
        {
            this.lazerImpact.UpdateColliderSize();
        }
        
        this.FadeOutLaser(this.lazerWidthBoost);
    }

    private void FadeOutLaser(float lazerWidthBoost)
    {
        if (!this.isShootingLazerBoost) return;

        this.timerLazerBoost += Time.deltaTime;

        float fadeFactor = Mathf.Clamp01(1 - this.timerLazerBoost / fadeDuration);

        this.ModelLazerCtrl.LineRenderer.startWidth = lazerWidthBoost * fadeFactor;
        this.ModelLazerCtrl.LineRenderer.endWidth = lazerWidthBoost * fadeFactor;

        if (this.timerLazerBoost >= fadeDuration)
        {
            ResetLaserState();
        }
    }

    private void ResetLaserState()
    {
        this.timerLazerBoost = 0f;
        this.ModelLazerCtrl.SetLazerState(this.lazerImpact,false);
        this.Active();
        this.isShootingLazerBoost = false;
        this.ModelLazerCtrl.LineRenderer.startWidth = this.originalWidth;
        this.ModelLazerCtrl.LineRenderer.endWidth = this.originalWidth;
        this.dameBoost = 0f;
        this.OriginalStartParticles();
    }

    protected virtual void BoostDame(float manaUsed)
    {
        if (manaUsed <= 0) return;

        this.manaUsedToBoost += manaUsed;

        if (this.manaUsedToBoost >= this.abilityData.ManaUse)
        {
            this.dameBoost += this.abilityData.ManaUse;
            this.manaUsedToBoost = 0;
        }
        this.isBoosting = true;
    }
    protected override void HandleLazerInput()
    {
        // Nếu boost đang ready và người chơi nhấn chuột phải thì ngăn không cho AbilityShootLazer hoạt động
        if (this.isReady && Input.GetMouseButton(1))
        {
            Debug.Log("StopLazer");
            StopLazer(); // Ngắt laser gốc
            return;
        }

        base.HandleLazerInput(); // Gọi lại xử lý mặc định từ AbilityShootLazer
    }
}
