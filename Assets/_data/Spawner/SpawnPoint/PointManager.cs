using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : GameMonoBehaviour
{

    protected static PointManager instance;
    public static PointManager Instance => instance;
    [SerializeField] protected List<Transform> bombPoints;
    public List<Transform> BombPoints => bombPoints;
    protected override void Awake()
    {
        base.Awake();
        if (PointManager.instance != null) Debug.LogWarning("Only 1 PointManager allow to exist");
        PointManager.instance = this;

    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBombPoint();
    }
    protected virtual void LoadBombPoint()
    {
        if (this.bombPoints != null || this.bombPoints.Count > 0) return;
        foreach (Transform point in this.transform.Find("PointsBomb"))
        {
            this.bombPoints.Add(point);
        }
    }
}
