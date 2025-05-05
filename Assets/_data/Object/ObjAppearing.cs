using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjAppearing : GameMonoBehaviour
{
    [Header("Obj Appearing")]
    [SerializeField] protected bool isAppearing = false;
    [SerializeField] protected bool appeared = false;
    [SerializeField] protected List<IObjAppearObserve> Observes = new List<IObjAppearObserve>();

    public bool IsAppearing => isAppearing;
    public bool Appeared => appeared;

    protected override void Start()
    {
        base.Start();
        this.OnAppearStart();
    }
    protected virtual void FixedUpdate()
    {
        this.Appearing();
    }

    protected abstract void Appearing();

    public virtual void Appear()
    {
        this.isAppearing = false;
        this.appeared = true;
        this.OnAppearFinish();
    }

    public virtual void ObservesAdd(IObjAppearObserve observe)
    {
        this.Observes.Add(observe);
    }
    protected virtual void OnAppearStart()
    {
        foreach(IObjAppearObserve observe in this.Observes)
        {
            observe.OnAppearStart();
        }
    }
    protected virtual void OnAppearFinish()
    {
        foreach (IObjAppearObserve observe in this.Observes)
        {
            observe.OnAppearFinish();
        }
    }
}
