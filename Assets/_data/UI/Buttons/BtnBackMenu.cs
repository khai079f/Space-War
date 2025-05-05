
using UnityEngine.SceneManagement;

public class BtnBackMenu : BaseButton
{
    private string sceneName = "MenuStartGameScene";

    protected override void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
