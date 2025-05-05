using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BntQuit : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

