using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICenterCtrl : BaseLoadUI
{
    [SerializeField] protected UIEvenLvUp evenLvUp;
    [SerializeField] protected UISetting setting;
    [SerializeField] protected UIEndGame UIEndGame;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIEvenLvUp();
        this.LoadUISetting();
        this.LoadUIEndGame();
    }

    protected virtual void LoadUIEvenLvUp()
    {
        if (evenLvUp != null) return;
        this.evenLvUp = transform.Find("UIEvenLevelUp").GetComponent<UIEvenLvUp>();
        this.evenLvUp.gameObject.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadUIEvenLvUp from UI Center", gameObject);

        if (evenLvUp == null) Debug.LogWarning(transform.name + ": LoadUIEvenLvUp from UI Center failed, Please check Active has states true", gameObject);
    }
    protected virtual void LoadUISetting()
    {
        if (setting != null) return;
        this.setting = transform.Find("UISetting").GetComponent<UISetting>();
        this.setting.gameObject.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadUISetting from UI Center", gameObject);
        if (setting == null) Debug.LogWarning(transform.name + ": LoadUISetting from UI Center failed, Please check Active has states true", gameObject);
    }
    protected virtual void LoadUIEndGame()
    {
        if (UIEndGame != null) return;
        this.UIEndGame = transform.Find("UIEndGame").GetComponent<UIEndGame>();
        this.UIEndGame.gameObject.SetActive(false);
        Debug.LogWarning(transform.name + ": UIEndGame from UI Center", gameObject);
        if (setting == null) Debug.LogWarning(transform.name + ": UIEndGame from UI Center failed, Please check Active has states true", gameObject);
    }

    public override void RegisterStartUI()
    {
        // Bật và tắt nhiều UI khác nhau
        this.ToggleUIForInitialization(this.evenLvUp.gameObject);
        this.ToggleUIForInitialization(this.setting.gameObject);
        this.ToggleUIForInitialization(this.UIEndGame.gameObject);

    }

}
