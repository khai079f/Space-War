using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : GameMonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager instance => _instance;
    [SerializeField] protected AudioSource backgoundAudio;
    [SerializeField] protected VFXAudioCtrl vFXAudioCtrl;
    [SerializeField] protected SOSetting SOVolume;
    public VFXAudioCtrl VFXAudioCtrl => vFXAudioCtrl;
    protected override void Awake()
    {
        base.Awake();
        if (AudioManager._instance != null)
        {
            Debug.LogError("Only one instance on scene");
            return;
        }
        AudioManager._instance = this;
    }
    protected override void Start()
    {
        base.Start();
        this.SetVolumeMusic(this.SOVolume.GetVolumeMusic());
        this.SetVolumeVFX(this.SOVolume.GetVolumeVFX());
        backgoundAudio.Play();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBackgoundAudio();
        this.LoadVFXAudioCtrl();
        this.LoadSOSetting();
    }
    protected virtual void LoadSOSetting()
    {
        if (this.SOVolume != null) return;
        string resPath = "Setting/SOSetting";
        this.SOVolume = Resources.Load<SOSetting>(resPath);
        Debug.Log(transform.name + ": LoadSOSetting", gameObject);
    }
    protected virtual void LoadBackgoundAudio()
    {
        if (this.backgoundAudio != null) return;
        this.backgoundAudio = transform.Find("BackgroundMusic").GetComponent<AudioSource>();
        Debug.Log(transform.name + " LoadBackgoundAudio",gameObject);
    }
    protected virtual void LoadVFXAudioCtrl()
    {
        if (this.vFXAudioCtrl != null) return;
        this.vFXAudioCtrl = transform.Find("VFXAudioCtrl").GetComponent<VFXAudioCtrl>();
        Debug.Log(transform.name + " LoadVFXAudioCtrl", gameObject);
    }

    public float GetVolumeMusic()
    {
        return this.SOVolume.GetVolumeMusic();
    }
    public float GetVolumeVFX()
    {
        return this.SOVolume.GetVolumeVFX();
    }
    public void SetVolumeMusic(float value)
    {
        this.SOVolume.SetVolumeMusic(Mathf.Clamp(value, 0f, 100f));
        // Chuyển đổi từ 0-100 sang 0-1
        this.backgoundAudio.volume = this.SOVolume.GetVolumeMusic() / 100f;
    }
    public void SetVolumeVFX(float value)
    {
        this.SOVolume.SetVolumeVFX(Mathf.Clamp(value, 0f, 100f));
        this.vFXAudioCtrl.UpdateVolumeVfxs(this.SOVolume.GetVolumeVFX());
    }
}
