using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointShooterWing : PointShooterManager
{
    [SerializeField] protected List<Transform> pointShooters;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPointShooters();
    }
    protected override void Start()
    {
        base.Start();
        this.UpdateStatuspointShooters(false, 2);
    }
    protected override void LoadParticleSystemSparks()
    {
        // Tìm tất cả các PointShooter trong WingSupport_1
        List<ParticleSystem> foundSparks = new List<ParticleSystem>();

        foreach (Transform child in transform)
        {
            if (!child.name.Contains("PointShooter")) continue;

            // Trong từng PointShooter, tìm các ParticleSystem trong Spark
            Transform sparkParent = child.Find("Spark");
            if (sparkParent != null)
            {
                ParticleSystem[] sparksInChild = sparkParent.GetComponentsInChildren<ParticleSystem>();
                foundSparks.AddRange(sparksInChild);
            }
        }

        // So sánh số lượng
        if (this.sparks.Count == foundSparks.Count) return;

        // Nếu khác → clear và load lại
        this.sparks.Clear();
        this.sparks.AddRange(foundSparks);

        Debug.LogWarning($"{transform.name}: Reloaded Sparks, total = {sparks.Count}", gameObject);
    }
    protected virtual void LoadPointShooters()
    {
        this.pointShooters.Clear();

        foreach (Transform child in transform)
        {
            if (!child.name.Contains("PointShooter")) continue;

            Transform pointShooter = child.GetComponent<Transform>();
            this.pointShooters.Add(pointShooter);

            // Set inactive sau khi load
            pointShooter.gameObject.SetActive(false);
        }

       // Debug.LogWarning($"{transform.name}: Loaded {pointShooters.Count} PointShooters and disabled them", gameObject);
    }

    public virtual void UpdateStatuspointShooters(bool status, int count)
    {
        if (this.pointShooters.Count <= 0) return;
        if (this.pointShooters.Count < count) count = (int)this.pointShooters.Count ;
            for (int i =0; i<count; i++)
        {
            if(this.pointShooters[i].gameObject.activeSelf != status) this.pointShooters[i].gameObject.SetActive(status);

        }
    }
    public virtual List<Transform> GetPointShooters()
    {
        List<Transform> activeShooters = new List<Transform>();

        foreach (Transform shooter in this.pointShooters)
        {
            if (shooter.gameObject.activeInHierarchy)
            {
                activeShooters.Add(shooter);
            }
        }

        return activeShooters;
    }


}
