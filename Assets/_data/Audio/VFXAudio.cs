using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXAudio : GameMonoBehaviour
{
    [SerializeField] protected AudioSource fXAudio;
    [SerializeField] private VFXAudioType audioType;
    public VFXAudioType AudioType => audioType;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAudioFXShootBullet();
        this.AutoDetectAudioType();
    }
    protected virtual void LoadAudioFXShootBullet()
    {
        if (this.fXAudio != null) return;
        this.fXAudio = GetComponent<AudioSource>();
        this.fXAudio.clip = Resources.Load<AudioClip>("Audio/"+transform.name);

        // Kiểm tra nếu không load được
        if (this.fXAudio == null)
        {
            Debug.LogError("Không tìm thấy AudioClip: Audio/FXShoot_0");
        }
    }
    protected virtual void AutoDetectAudioType()
    {
        if (System.Enum.TryParse(transform.name, out VFXAudioType parsedType))
        {
            this.audioType = parsedType;
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy loại AudioType tương ứng với tên: {transform.name}", gameObject);
        }
    }
    public virtual void PlayAudioFX()
    {
        if (this.fXAudio != null && this.fXAudio.clip != null)
        {
            this.fXAudio.Play();
        }
        else
        {
            Debug.LogWarning("Không thể phát âm thanh: AudioSource hoặc AudioClip null.");
        }
    }
    public virtual void UpdateVolumeVfx(float value)
    {
        // Đảm bảo value nằm trong khoảng 0 đến 100
        value = Mathf.Clamp(value, 0f, 100f);
        // Chuyển đổi từ 0-100 sang 0-1
        this.fXAudio.volume = value / 100f;
    }
}
