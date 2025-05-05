using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXAudioCtrl : GameMonoBehaviour
{
    [SerializeField] protected List<VFXAudio> VFXAudios;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAllVFXAudio();
    }
    protected virtual void LoadAllVFXAudio()
    {
        // Nếu danh sách đã có giá trị, xóa nó đi
        if (this.VFXAudios != null && this.VFXAudios.Count > 0)
            this.VFXAudios.Clear();

        VFXAudios = new List<VFXAudio>(transform.GetComponentsInChildren<VFXAudio>());
       // Debug.Log($"{transform.name} Loaded {VFXAudios.Count} VFXAudio components.", gameObject);
    }
    public VFXAudio GetVFXAudio(VFXAudioType type)
    {
        return VFXAudios.Find(vfx => vfx.AudioType == type);
    }

    public virtual void UpdateVolumeVfxs(float value)
    {
        foreach(VFXAudio vFXAudio in VFXAudios)
        {
            vFXAudio.UpdateVolumeVfx(value);
        }
    }
}