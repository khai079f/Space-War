using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLazerCtrl : GameMonoBehaviour
{
    [SerializeField] protected LineRenderer lineRenderer;
    public LineRenderer LineRenderer => lineRenderer;
    [SerializeField] protected Transform startVFX;
    public Transform StartVFX => startVFX;
    [SerializeField] protected Transform endVFX;
    public Transform EndVFX => endVFX;
    [SerializeField] protected List<ParticleSystem> startParticles = new List<ParticleSystem>();
    public List<ParticleSystem> StartParticles => startParticles;
    [SerializeField] protected List<ParticleSystem> endParticles = new List<ParticleSystem>();
    public List<ParticleSystem> EndParticles => endParticles;
    [SerializeField]
    private Color[] levelColors = new Color[]
    {
    new Color(1.0f, 0.75f, 0.3f),  // Level 1 - Vàng cam nhạt
    new Color(1.0f, 0.55f, 0.1f),  // Level 2 - Cam sáng
    new Color(1.0f, 0.4f, 0.0f),   // Level 3 - Cam đậm
    new Color(1.0f, 0.3f, 0.0f),   // Level 4 - Đỏ cam
    new Color(1.2f, 0.2f, 0.0f),   // Level 5 - Đỏ cam cháy sáng (HDR phát sáng)
    };

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLineRenderer();
        this.LoadParticleSystem();
    }
    protected virtual void LoadLineRenderer()
    {
        if (this.lineRenderer != null) return;
        this.lineRenderer = transform.GetComponent<LineRenderer>();
        Debug.LogWarning(transform.name + ":LoadLineRenderer for lazer", gameObject);
    }
    protected virtual void LoadParticleSystem()
    {
        if (this.startParticles.Count > 0 && this.startVFX != null) return;
        this.startVFX = transform.Find("StartVFX").GetComponent<Transform>();
        ParticleSystem[] startParticles = transform.Find("StartVFX").GetComponentsInChildren<ParticleSystem>();
        this.startParticles.AddRange(startParticles);
        if (this.endParticles.Count > 0 && this.endVFX != null) return;
        this.endVFX = transform.Find("EndVFX").GetComponent<Transform>();
        ParticleSystem[] endParticles = transform.Find("EndVFX").GetComponentsInChildren<ParticleSystem>();
        this.endParticles.AddRange(endParticles);
        Debug.LogWarning(transform.name + ":LoadParticleSystem for lazer", gameObject);
    }
    public virtual void SetLazerState(ImpactAbstract lazerImpact, bool startPoint = false, bool endPoint = false, bool lineLazer = false)
    {
        // Enable or disable the laser line
        lineRenderer.enabled = lineLazer;

        // Manage start VFX particles
        bool shouldPlayStart = startPoint || lineLazer;
        foreach (var ps in startParticles)
        {
            if (shouldPlayStart)
            {
                if (!ps.isPlaying) ps.Play();
            }
            else
            {
                if (ps.isPlaying) ps.Stop();
            }
        }

        // Manage end VFX particles
        bool shouldPlayEnd = endPoint || lineLazer;
        foreach (var ps in endParticles)
        {
            if (shouldPlayEnd)
            {
                if (!ps.isPlaying) ps.Play();
            }
            else
            {
                if (ps.isPlaying) ps.Stop();
            }
        }

        // Toggle collider on the laser impact
        lazerImpact.SetColliderState(lineLazer);
    }
    public void UpdateLaserColorByLevel(int level)
    {
        // Nếu level vượt quá độ dài mảng, dùng màu cuối cùng
        int index = level - 1;
        if (index >= levelColors.Length)
            index = levelColors.Length - 1;

        Color color = levelColors[index];

        if (lineRenderer != null && lineRenderer.material != null)
        {
            lineRenderer.material.EnableKeyword("_EMISSION");
            lineRenderer.material.SetColor("Color_Lazer", color);
            lineRenderer.material.SetColor("_EmissionColor", color);
        }
    }
    public void UpdateLaserWidth(float width)
    {
        if (this.lineRenderer != null)
        {
            this.lineRenderer.startWidth = width;
            this.lineRenderer.endWidth = width;
        }
    }
    public virtual void UpdateStartVFX(float newRate, Color? newColor = null, float? newBeamSize = null)
    {
        UpdateParticlesRate(newRate);
        
        if (newColor.HasValue)
        {
            UpdateParticlesColor(newColor.Value);
        }
        
        if (newBeamSize.HasValue)
        {
            UpdateBeamSize(newBeamSize.Value);
        }
    }
    
    protected virtual void UpdateParticlesRate(float newRate)
    {
        foreach (var particle in startParticles)
        {
            if (particle.name == "Particles")
            {
                var emission = particle.emission;
                var rate = emission.rateOverTime;
                rate.constant = newRate;
                emission.rateOverTime = rate;
            }
        }
    }
    
    protected virtual void UpdateParticlesColor(Color newColor)
    {
        foreach (var particle in startParticles)
        {
            if (particle.name == "Particles")
            {
                var mainModule = particle.main;
                mainModule.startColor = newColor;
            }
            else if (particle.name == "Beam")
            {
                var mainModule = particle.main;
                mainModule.startColor = newColor;
            }
        }
    }
    
    protected virtual void UpdateBeamSize(float newBeamSize)
    {
        foreach (var particle in startParticles)
        {
            if (particle.name == "Beam")
            {
                var mainModule = particle.main;
                var startSizeCurve = mainModule.startSize;
                
                // Đặt kích thước ngẫu nhiên dựa trên giá trị newBeamSize
                float minSize, maxSize;
                
                if (newBeamSize > 1.0f)
                {
                    // Khi nhấn chuột: kích thước lớn hơn (1.5 - 2.0)
                    minSize = 1.5f;
                    maxSize = 2.0f;
                }
                else
                {
                    // Khi thả chuột: kích thước nhỏ hơn (0.5 - 1.5)
                    minSize = 0.5f;
                    maxSize = 1.5f;
                }
                
                startSizeCurve.constantMin = minSize;
                startSizeCurve.constantMax = maxSize;
                mainModule.startSize = startSizeCurve;
            }
        }
    }
}

