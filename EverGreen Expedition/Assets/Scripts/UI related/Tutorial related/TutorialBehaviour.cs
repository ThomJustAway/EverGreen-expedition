using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject tutorialPanel;
    [SerializeField] protected Image tutorialImage;
    [SerializeField] protected TextMeshProUGUI tutorialText;
    [SerializeField] protected GameObject backButton;
    [SerializeField] protected TutorialData[] tutorialMessages;
    protected int index;
    private  void Start()
    {
        DoTutorialChecking();
    }

    protected virtual void DoTutorialChecking()
    {
        if (!GameManager.Instance.HasStartedTutorial)
        {
            tutorialPanel.SetActive(true);
            index = 0;
            GameManager.Instance.HasPlayedTutorial();
            ShowMessage();
        }
        else
        {
            tutorialPanel.SetActive(false);
        }
    }

    public void OnBackClick()
    {
        index--;
        ShowMessage();
    }

    public virtual void OnContinueClick()
    {
        index++;
        if(index == tutorialMessages.Length)
        {//close the tab as there is no more to show
            tutorialPanel.SetActive(false); 
        }
        else
        {
            ShowMessage();
        }
    }

    protected void ShowMessage()
    {
        var data = tutorialMessages[index]; 
        tutorialImage.sprite = data.image;
        tutorialImage.preserveAspect = true;

        tutorialText.text = data.message.Trim();

        if(index == 0)
        {
            backButton.SetActive(false);
        }
        else 
        {
            backButton.SetActive(true);
        }
    }

    public void OnCloseTutorial()
    {
        Time.timeScale = 1f;
        tutorialPanel.SetActive(false );
    }
}

[Serializable]
public struct TutorialData
{
    public Sprite image;
    [TextArea(3 , 5)]
    public string message;
}