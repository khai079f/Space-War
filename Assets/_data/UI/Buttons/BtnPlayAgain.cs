using UnityEngine.SceneManagement;

public class BtnPlayAgain : BaseButton
{
    private string sceneName = "GamePlay";

    protected override void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
