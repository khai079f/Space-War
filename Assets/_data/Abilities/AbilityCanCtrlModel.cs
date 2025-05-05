using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCanCtrlModel
{
    // Lưu trữ danh sách các giá trị ban đầu của ParticleSystem
    protected List<ParticleSystem> particleFires = new List<ParticleSystem>();
    protected List<ParticleSystemData> originalValues = new List<ParticleSystemData>();

    public virtual void ChangeColorSprite(SpriteRenderer sprite, Color color)
    {
        sprite.color = color;
    }

    public virtual void ChangeColorParticle(List<ParticleSystem> particleFires, Color color = default, float size = 1f)
    {
        this.particleFires = particleFires;
        // Đặt màu mặc định nếu không truyền
        if (color == default)
            color = Color.blue;

        // Lặp qua từng ParticleSystem trong danh sách
        foreach (var particle in particleFires)
        {
            if (particle == null) continue;

            // Lấy MainModule của ParticleSystem
            var mainModule = particle.main;

            // Đặt StartColor
            mainModule.startColor = color;

            // Đặt StartSize
            mainModule.startSize = size;
        }
    }

    // Lưu các giá trị ban đầu của startColor và startSize
    public void SaveOriginalValues(List<ParticleSystem> particleFires)
    {
        originalValues.Clear();  // Xóa các giá trị cũ nếu có
        foreach (var particle in particleFires)
        {
            if (particle == null) continue;

            // Lấy MainModule của ParticleSystem
            var mainModule = particle.main;

            // Lưu lại các giá trị ban đầu
            originalValues.Add(new ParticleSystemData(mainModule.startColor.color, mainModule.startSize.constant));
        }
    }

    // Khôi phục lại giá trị ban đầu của tất cả các ParticleSystem
    public virtual void RestoreOriginalValues()
    {
        int index = 0;
        foreach (var particle in particleFires)
        {
            if (particle == null || index >= originalValues.Count) continue;

            // Lấy MainModule của ParticleSystem
            var mainModule = particle.main;

            // Khôi phục lại startColor và startSize
            mainModule.startColor = originalValues[index].startColor;
            mainModule.startSize = originalValues[index].startSize;

            index++;
        }
    }
}

// Lớp lưu trữ dữ liệu của ParticleSystem
public class ParticleSystemData
{
    public Color startColor;
    public float startSize;

    public ParticleSystemData(Color startColor, float startSize)
    {
        this.startColor = startColor;
        this.startSize = startSize;
    }
}
