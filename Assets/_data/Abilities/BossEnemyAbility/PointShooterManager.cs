using UnityEngine;
using System.Collections.Generic;

public class PointShooterManager : GameMonoBehaviour
{
    [SerializeField] protected List<ParticleSystem> sparks = new List<ParticleSystem>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadParticleSystemSparks();
    }

    protected virtual void LoadParticleSystemSparks()
    {
        Transform sparkParent = transform.Find("Spark");
        if (sparkParent == null)
        {
            Debug.LogWarning($"{transform.parent.name}: Spark object not found", gameObject);
            return;
        }

        ParticleSystem[] foundSparks = sparkParent.GetComponentsInChildren<ParticleSystem>();

        if (this.sparks.Count == foundSparks.Length) return;

        // Nếu khác số lượng, thì xóa và load lại
        this.sparks.Clear();
        this.sparks.AddRange(foundSparks);

        Debug.LogWarning($"{transform.parent.name}: Reloaded Sparks, total = {sparks.Count}", gameObject);
    }


    public virtual void OnSpark()
    {
        if (this.sparks == null || this.sparks.Count == 0) return;

        foreach (var spark in sparks)
        {
            if (spark == null) continue;
            spark.Play();
        }
    }
}
