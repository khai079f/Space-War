using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BntBackSelectShip : BaseButton
{
    protected string sceneName = "MenuStartGameScene";
    protected override void OnClick()
    {
       // Debug.Log("LoadScene GamePlay");
        SceneManager.LoadScene(this.sceneName);
    }
}
