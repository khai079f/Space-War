using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityElectrofield : AbilitylethalityShipPlayer
{
    [Header("Ability Electrofield")]
    [SerializeField] protected ParticleSystem modelElectrofield;
    [SerializeField] protected ElectrofieldImpact electrofieldImpact;
    public float LargestParticleSize { get; private set; } = 0f;
    private ParticleSystem.Particle[] particles;
    private float lastDamageTime;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadElectrofield();
    }
    protected override void Start()
    {
        base.Start();
        
    }
    public override void LoadShipPlayAbleAbilities()
    {
        base.LoadShipPlayAbleAbilities();
        if (this.AbilityImpact == null) return;
        this.electrofieldImpact = (ElectrofieldImpact)this.AbilityImpact;
    }
    protected virtual void LoadElectrofield()
    {
        if (this.modelElectrofield != null) return;
        this.modelElectrofield = transform.Find("Electric").GetComponent<ParticleSystem>();
        Debug.LogWarning(transform.name + ":LoadElectrofield", gameObject);
    }

    private void LateUpdate()
    {
        this.LargestParticleSize = this.GetLargestParticleSize();
    }
    protected virtual float GetLargestParticleSize()
    {
        if (modelElectrofield == null) return 0f;

        // Lấy tất cả các hạt hiện tại
        if (particles == null || particles.Length < modelElectrofield.main.maxParticles)
            particles = new ParticleSystem.Particle[modelElectrofield.main.maxParticles];

        int particleCount = modelElectrofield.GetParticles(particles);

        if (particleCount == 0) return 0f; // Không có hạt nào

        // Tìm kích thước lớn nhất
        float largestSize = particles[0].GetCurrentSize(modelElectrofield);

        for (int i = 1; i < particleCount; i++)
        {
            float particleSize = particles[i].GetCurrentSize(modelElectrofield);

            if (particleSize > largestSize)
            {
                largestSize = particleSize;
            }
        }
        return largestSize;
    }
    public void DameByTime(Collider2D other)
    {
        if (other == null) return;
        if (Time.time - lastDamageTime >= this.delay)
        {
            lastDamageTime = Time.time;
            DameSender.Send(other.transform);  // Gọi phương thức gây sát thương
        }
    }
    public void UpdateElectrofield(float width, int count)
    {
        if (this.modelElectrofield == null) return;

        // Cập nhật kích thước hạt (startSize)
        var mainModule = this.modelElectrofield.main;
        mainModule.startSize = mainModule.startSize.constant + width;

        // Cập nhật tốc độ phát hạt (rateOverTime)
        var emissionModule = this.modelElectrofield.emission;
        emissionModule.rateOverTime = emissionModule.rateOverTime.constant + count;
    }
    public override void WhatlevelUpStrategy()
    {
        this.levelUpStrategy = new ElectrofieldLeveUp(this);
    }
}
