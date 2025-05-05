using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndGame : BaseUI
{
    private static UIEndGame instance;
    public static UIEndGame Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (UIEndGame.instance != null) Debug.LogError("Only 1 UIInventory allow to exist");
        UIEndGame.instance = this;
    }
}
