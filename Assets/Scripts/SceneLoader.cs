using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "SceneLoader", menuName = "ScriptableObjects/SceneLoader")]

public class SceneLoader : ScriptableObject
{
    public void LoadTitleScreenScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadGameWorldScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("MainGame");
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    public void LoadWinScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("WinScene");
    }

    public void LoadGameOverScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("LoseScene");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();

    }

}
