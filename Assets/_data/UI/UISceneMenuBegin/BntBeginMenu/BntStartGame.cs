using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BntStartGame : BaseButton
{
    protected string sceneName = "CharacterSelection";
    protected override void OnClick()
    {
       // Debug.Log("LoadScene GamePlay");
        SceneManager.LoadScene(this.sceneName);
    }
}
