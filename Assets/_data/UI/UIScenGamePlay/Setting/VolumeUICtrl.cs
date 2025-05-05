using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeUICtrl : GameMonoBehaviour
{
    [SerializeField] protected VolumeGame volumeMusic;
    [SerializeField] protected VolumeGame volumeVFX;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadVolumeMusic();
        this.LoadVolumeVFX();
    }
    protected virtual void LoadVolumeMusic()
    {
        if (this.volumeMusic != null) return;
        this.volumeMusic = transform.Find("VolumeMusic").GetComponent<VolumeGame>();
        Debug.Log(transform.name + " LoadSliderVolume", gameObject);
    }
    protected virtual void LoadVolumeVFX()
    {
        if (this.volumeVFX != null) return;
        this.volumeVFX =transform.Find("VolumeVFX").GetComponent<VolumeGame>();
        Debug.Log(transform.name + " LoadVolumeVFX ", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.SetValueForVolume();
    }
    protected virtual void SetValueForVolume()
    {
        this.volumeMusic.SetValueForSliderVolume(AudioManager.instance.GetVolumeMusic());
        this.volumeVFX.SetValueForSliderVolume(AudioManager.instance.GetVolumeVFX());
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        if(AudioManager.instance!= null) this.SetValueForVolume();

    }
    public virtual void UpdateVolumeFromSlider()
    {
        AudioManager.instance.SetVolumeMusic(this.volumeMusic.GetValueForSliderVolume());
        AudioManager.instance.SetVolumeVFX(this.volumeVFX.GetValueForSliderVolume());
    }

}
