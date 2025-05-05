using UnityEngine;
using UnityEngine.UI;

public class SliderStatus : BaseSlider
{
    [Header("Status")]
    [SerializeField] protected float maxValue = 100f;
    [SerializeField] protected float currentValue = 70f;

    protected override void FixedUpdate()
    {
        this.ValueShowing();
    }

    protected virtual void ValueShowing()
    {
        float percent = this.currentValue / this.maxValue;
        this.slider.value = percent;
    }
    protected override void ValueChanged(float newValue)
    {
        //
    }

    public virtual void UpdateMaxValue(float MaxValue)
    {
        this.maxValue = MaxValue;
    }
    public virtual void UpdateCurrentValue(float currentValue)
    {
      //  Debug.Log("UpdateCurrentValue: "+ currentValue);

        this.currentValue = currentValue;
      //  Debug.Log("UpdateCurrentValue: " + this.currentValue);
    }
}