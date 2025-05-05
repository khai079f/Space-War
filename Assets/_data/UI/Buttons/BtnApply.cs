using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnApply : BaseButton
{
    protected override void OnClick()
    {
        UISetting.Instance.VolumeUI.UpdateVolumeFromSlider();
        UISetting.Instance.Toggle();
    }
}
