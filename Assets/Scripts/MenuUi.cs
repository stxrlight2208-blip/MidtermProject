using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu Scene");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("Level2");
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}