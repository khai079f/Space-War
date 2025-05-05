using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLevelByTime : Level
{
    [SerializeField] private float timePerLevel =60f; // Thời gian mỗi cấp độ (giây)
    private float elapsedTime = 0f; // Thời gian đã trôi qua
                                    // Singleton pattern to ensure only one instance of JunkSpawner exists
    protected static MapLevelByTime instance;
    public static MapLevelByTime Instance => instance;
    // Event khi LevelUp
    public event Action OnLevelUp; // Truyền cấp độ mới qua event

    protected override void Awake()
    {
        base.Awake();
        if (MapLevelByTime.instance != null)
        {
            Debug.LogWarning("Only 1 MapLevelByTime allowed to exist");
            return; // Dừng lại nếu đã có một instance khác
        }
        MapLevelByTime.instance = this;
    }
    protected virtual void FixedUpdate()
    {
        this.UpdateLevelOverTime();
    }
    protected virtual void UpdateLevelOverTime()
    {
        if(this.levelCurrent ==0) this.LevelUp();
        this.elapsedTime += Time.fixedDeltaTime;

        if (this.elapsedTime >= this.timePerLevel)
        {
            this.elapsedTime = 0f;
            this.LevelUp();
        }
    }
    public override void LevelUp()
    {
        base.LevelUp();
        OnLevelUp?.Invoke(); // Phát event khi LevelUp
    }
}

