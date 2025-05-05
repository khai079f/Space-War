using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BntConfirmSelectShip : BaseButton
{
    protected string sceneName = "LoadingScreen";
    protected override void OnClick()
    {
       // Debug.Log("LoadScene GamePlay");
        SceneManager.LoadScene(this.sceneName);
    }
}
