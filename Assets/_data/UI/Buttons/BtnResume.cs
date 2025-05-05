using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnResume : BaseButton
{
    protected override void OnClick()
    {
        UISetting.Instance.Toggle();
    }
}
