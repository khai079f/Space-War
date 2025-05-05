using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSlider : GameMonoBehaviour
{
    [Header("Base Slider")]
    [SerializeField] protected Slider slider;

    protected virtual void FixedUpdate()
    {
        // for override
    }
    protected override void Start()
    {
        base.Start();
        this.AddOnClickEvent();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadslider();
    }

    protected virtual void Loadslider()
    {
        if (this.slider != null) return;
        this.slider = GetComponent<Slider>();
        Debug.Log(transform.name + " Loadslider", gameObject);
    }

    protected virtual void AddOnClickEvent()
    {
        this.slider.onValueChanged.AddListener(this.ValueChanged);
    }

    protected abstract void ValueChanged(float newValue);

}