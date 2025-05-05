using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnemyAttackLazer : AbilityEnemyLazerCtrl
{
    [SerializeField] protected float maxLaserDistance = 15f;
    private float laserWidth = 0.15f;
    private bool isDrawIndicator = true;
    [SerializeField] private float timerLazer = 0;
    private float timeShootWarning = 2f;
    protected override void Start()
    {
        base.Start();
        this.SetLazerState(false);
        this.drawSkillIndicator.UpdateRange(this.maxLaserDistance, new Vector3(this.laserWidth, 0.95f, 1));
    }

    public virtual void ShootingLazer()
    {
        if (!this.isReady) return; // Dừng coroutine nếu isReady = false
        this.ShootWarningLazer();
        this.ShootLazer();
    }

    // Bắn laser cảnh báo
    private void ShootWarningLazer()
    {
        if (!isDrawIndicator) return;
        this.drawSkillIndicator.StateDrawSkillIndicator(true);
        this.TimingShootWarning();

    }
    // Bắn laser chính
    private void ShootLazer()
    {
        if (this.isDrawIndicator) return;
        this.SetLazerState(true);
        this.SetStatusLazer();
        this.lazerImpact.UpdateColliderSize();
        this.FadeOutLaser(laserWidth); // Làm mờ dần và tắt laser
    }
    // Cập nhật trạng thái và vị trí của laser
    private void SetStatusLazer()
    {
        this.modelLazerCtrl.LineRenderer.startWidth = laserWidth;
        this.modelLazerCtrl.LineRenderer.endWidth = laserWidth;
        this.modelLazerCtrl.LineRenderer.SetPosition(0, shootPoint.position);
        // Tính toán điểm cuối của laser
        Vector2 direction = shootPoint.up * -1; // Hướng của laser
        Vector2 endPosition = (Vector2)shootPoint.position + direction.normalized * this.maxLaserDistance;
        this.modelLazerCtrl.LineRenderer.SetPosition(1, endPosition);
        // Đặt vị trí VFX bắt đầu và kết thúc
        this.modelLazerCtrl.StartVFX.transform.position = shootPoint.position;
    }

    protected virtual void TimingShootWarning()
    {
        this.timerLazer += Time.deltaTime;
        this.drawSkillIndicator.ChangeHeightByTime(this.timerLazer,this.timeShootWarning);
        if (this.timerLazer < this.timeShootWarning) return;
        this.isDrawIndicator = false;
        this.drawSkillIndicator.StateDrawSkillIndicator(false);
        this.timerLazer = 0;

    }
    private void FadeOutLaser(float initialWidth)
    {
        this.timerLazer += Time.deltaTime;
        if (this.timerLazer <= 1f)
        {
            float currentWidth = Mathf.Lerp(initialWidth, 0f, this.timerLazer / 1f);
            this.modelLazerCtrl.LineRenderer.startWidth = currentWidth;
            this.modelLazerCtrl.LineRenderer.endWidth = currentWidth;
        }
        else
        {
            this.timerLazer = 0f;
            SetLazerState(false); // Tắt laser
            this.isDrawIndicator = true;
            this.Active();
        }

    }

    // Bật/tắt laser và hiệu ứng liên quan
    private void SetLazerState(bool state)
    {
        this.modelLazerCtrl.LineRenderer.enabled = state;
        this.modelLazerCtrl.StartVFX.transform.gameObject.SetActive(state);
        lazerImpact.transform.gameObject.SetActive(state);
    }
}
