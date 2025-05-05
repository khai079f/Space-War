using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : GameMonoBehaviour
{
    protected bool isPaused = false; // Biến để lưu trạng thái pause
    protected bool isOpen = false;
    public virtual void Toggle()
    {
        this.isOpen = !this.isOpen;
        if (this.isOpen) this.Open();
        else this.Close();
    }
    public virtual void Pause()
    {
        if (isPaused) return; // Nếu game đã tạm dừng, không cần tạm dừng lại
        Time.timeScale = 0f; // Dừng game
        isPaused = true;
        Debug.Log("Game is paused.");
    }

    public virtual void Resume()
    {
        if (!isPaused) return; // Nếu game chưa tạm dừng, không cần tiếp tục lại
        Time.timeScale = 1f; // Tiếp tục game
        isPaused = false;
        Debug.Log("Game is resumed.");
    }

    public virtual void Open()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            this.Pause();
        }
    }

    public virtual void Close()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            this.Resume(); 
        }
    }

    protected override void OnDisable()
    {
        this.Resume();
    }
}
