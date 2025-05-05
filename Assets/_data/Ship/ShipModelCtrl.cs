using UnityEngine;
using System.Collections.Generic;

public class ShipModelCtrl : BaseModel
{
    [SerializeField] protected List<ParticleSystem> particleFires;

    private float currentEnginePower = 0.5f; // Mức động cơ hiện tại
    private float targetEnginePower = 0.5f; // Mức động cơ mục tiêu

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadParticleSystem();
    }

    protected virtual void LoadParticleSystem()
    {
        // Lấy tất cả ParticleSystem ở đây
        ParticleSystem[] foundParticles = transform.GetComponentsInChildren<ParticleSystem>();

        // Nếu số lượng tìm thấy bằng số lượng đã có thì không cần làm gì
        if (foundParticles.Length == this.particleFires.Count) return;

        // Nếu khác, clear rồi add lại
        this.particleFires.Clear();
        this.particleFires.AddRange(foundParticles);

        Debug.Log($"{transform.name}: Loaded {particleFires.Count} ParticleSystem(s)", gameObject);
    }


    public virtual void SetTargetEnginePower(float engine)
    {
        // Thiết lập mức động cơ mục tiêu
        this.targetEnginePower = engine;
    }
    public void SetEngineColor(Color color)
    {
        foreach (var ps in particleFires)
        {
            var main = ps.main;
            main.startColor = new ParticleSystem.MinMaxGradient(color);
        }
    }
    private void Update()
    {
        // Cập nhật currentEnginePower dần đến targetEnginePower theo thời gian
        currentEnginePower = Mathf.Lerp(currentEnginePower, targetEnginePower, Time.deltaTime); // 2f là tốc độ thay đổi
        ApplyEnginePower(currentEnginePower); // Áp dụng mức động cơ hiện tại
    }

    protected virtual void ApplyEnginePower(float engine)
    {
        if (particleFires == null || particleFires.Count == 0) return;

        foreach (var particle in particleFires)
        {
            var main = particle.main;
            main.startLifetime = engine; // Điều chỉnh thời gian sống của hạt theo mức động cơ
        }
    }

}
