using Assets.Scripts.Data_manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelScene : MonoBehaviour
{
    public void GoStartingMenuScene()
    {
        SceneManager.LoadScene(SceneName.CreateCharacterLevel);
    }
}
