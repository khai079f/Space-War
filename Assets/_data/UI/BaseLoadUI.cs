using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseLoadUI : GameMonoBehaviour
{

    public abstract void RegisterStartUI();
    protected virtual void ToggleUIForInitialization(GameObject uiGameObject)
    {
        if (uiGameObject == null)
        {
            Debug.LogError("UI GameObject is not set!");
            return;
        }
        // Bật tạm thời
        uiGameObject.SetActive(true);

        // Thực hiện các tác vụ nếu cần

        // Tắt ngay lập tức
        uiGameObject.SetActive(false);
    }
}
