using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCtrl : BaseModel
{
    public SpriteRenderer Sprite => spriteRenderer;
    [SerializeField] protected List<ParticleSystem> particleFires = new List<ParticleSystem>();
    public List<ParticleSystem> ParticleFires => particleFires;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadParticleSystem();
    }



    protected virtual void LoadParticleSystem()
    {
        if (this.particleFires.Count > 0) return;
        if (transform.childCount <= 0) return;
        foreach (Transform particleSystem in transform)
        {
            ParticleSystem particleFire = particleSystem.GetComponent<ParticleSystem>();
            if (particleFire == null) return;
            this.particleFires.Add(particleFire);
        }
        Debug.LogWarning(transform.name + ": LoadParticleSystem", gameObject);
    }
    public virtual void SetParticleFires(List<ParticleSystem> particleFires)
    {
        if (particleFires == null) return;
        this.particleFires = particleFires;
    }
}
