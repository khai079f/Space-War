using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : GameMonoBehaviour
{
    [SerializeField] private Slider progressBar; // Thanh tiến trình

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadProgressBar();
    }
    protected virtual void LoadProgressBar()
    {
        if (this.progressBar != null) return;
        this.progressBar = GetComponent<Slider>();
        Debug.Log(transform.name+ " LoadProgressBar",gameObject);
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(LoadSceneAsync("GamePlay"));
    }
    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // Chặn kích hoạt ngay lập tức

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // Tiến trình từ 0 -> 1
            progressBar.value = progress;

            if (operation.progress >= 0.9f) // Đợi xong
            {
                yield return new WaitForSeconds(1f); // Fake loading time
                operation.allowSceneActivation = true; // Kích hoạt Scene mới
            }

            yield return null;
        }
    }
}
