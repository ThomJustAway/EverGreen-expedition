using Assets.Scripts.Data_manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelScene : MonoBehaviour
{
    public void GoStartingMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneName.CreateCharacterLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();

        SceneManager.LoadScene(SceneName.StartingLevel);
    }
}
