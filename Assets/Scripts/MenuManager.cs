using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Play button clicked!");

        SceneManager.LoadScene("GameScene");

    }

    public void Settings()
    {
        Debug.Log("Settings button clicked!");

        SceneManager.LoadScene("SettingsScene");

    }

}