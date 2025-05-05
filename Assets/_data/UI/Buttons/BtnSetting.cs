using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BtnSetting : BaseButton
{
    protected override void OnClick()
    {
        UISetting.Instance.Toggle();
    }
}
