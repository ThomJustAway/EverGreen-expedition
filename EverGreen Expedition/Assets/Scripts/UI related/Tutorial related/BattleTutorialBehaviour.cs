using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI_related.Tutorial_related
{
    public class BattleTutorialBehaviour : TutorialBehaviour
    {
        protected override void DoTutorialChecking()
        {
            if (!GameManager.Instance.HasStartedBattleTutorial)
            {
                Time.timeScale = 0f; //stop the game
                tutorialPanel.SetActive(true);
                index = 0;
                GameManager.Instance.HasPlayedBattleTutorial();
                ShowMessage();
            }
            else
            {
                tutorialPanel.SetActive(false);
            }
        }

        public override void OnContinueClick()
        {
            index++;
            if (index == tutorialMessages.Length)
            {//close the tab as there is no more to show
                Time.timeScale = 1f;
                tutorialPanel.SetActive(false);
            }
            else
            {
                ShowMessage();
            }
        }

        
    }
}