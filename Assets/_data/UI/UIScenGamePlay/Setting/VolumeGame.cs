using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeGame : GameMonoBehaviour
{
    [SerializeField] protected Slider sliderVolume;
    [SerializeField] protected Image imageButton;
    [SerializeField] protected BntIconVolume bntIconVolume;
    protected float currentVolume;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSliderVolume();
        this.LoadImageButton();
        this.LoadButton();
    }
    protected virtual void LoadSliderVolume()
    {
        if (this.sliderVolume != null) return;
        this.sliderVolume = transform.GetComponentInChildren<Slider>();
        Debug.Log(transform.name+ " LoadSliderVolume",gameObject);
        this.sliderVolume.onValueChanged.AddListener(OnSliderValueChanged);
    }
    protected virtual void LoadImageButton()
    {
        if (this.imageButton != null) return;
        this.imageButton = transform.GetComponentInChildren<Image>();
        Debug.Log(transform.name + " LoadImageButton", gameObject);
    }
    protected virtual void LoadButton()
    {
        if (this.bntIconVolume != null) return;
        this.bntIconVolume = transform.GetComponentInChildren<BntIconVolume>();
        Debug.Log(transform.name + " LoadButton", gameObject);
    }

    public virtual void SetValueForSliderVolume(float value)
    {
        value = Mathf.Clamp(value, 0f, 100f);
        // Chuyển đổi từ 0-100 sang 0-1
        this.sliderVolume.value = value / 100f;
        this.currentVolume = value;
    }
    public virtual void MuteVolume()
    {

        this.sliderVolume.value = 0;
    }
    public virtual float GetcurrentVolume()
    {
        return this.currentVolume;
    }
    public virtual float GetValueForSliderVolume()
    {
        this.currentVolume = Mathf.Clamp(this.sliderVolume.value * 100f, 0f, 100f);
        return Mathf.Clamp(this.sliderVolume.value * 100f,0f,100f);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        if (sliderVolume != null)
        {
            sliderVolume.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if (sliderVolume != null)
        {
            sliderVolume.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
    protected void OnSliderValueChanged(float value)
    {
        this.bntIconVolume.SetImageByValue(value);
    }
}
