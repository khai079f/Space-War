using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrl : GameMonoBehaviour
{
    protected static GameCtrl instance;
    public static GameCtrl Instance { get => instance; }

    [SerializeField] protected Camera mainCamera;
    public Camera MainCamera { get => mainCamera; }

    protected override void Awake()
    {
        base.Awake();
        if( instance != null)
        {
            Debug.LogError("only 1 GameCtrl allow to sencen");
            return;
        }
        GameCtrl.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMainCamera();
    }

    protected virtual void LoadMainCamera()
    {
        if (mainCamera != null) return;
        this.mainCamera = FindFirstObjectByType<Camera>();
        Debug.Log(transform.name + ": LoadmainCamera", gameObject);
    }
}
