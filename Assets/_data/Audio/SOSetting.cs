using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOSetting", menuName = "SO/Setting")]
public class SOSetting : ScriptableObject
{
    [SerializeField] private float volumeMusic = 50;
    [SerializeField] private float volumeVFX = 50;
    public float GetVolumeMusic()
    {
        return this.volumeMusic;
    }
    public float GetVolumeVFX()
    {
        return this.volumeVFX;
    }
    public void SetVolumeMusic(float value)
    {
        this.volumeMusic = value;
    }
    public void SetVolumeVFX(float value)
    {
        this.volumeVFX = value; 
    }
}
