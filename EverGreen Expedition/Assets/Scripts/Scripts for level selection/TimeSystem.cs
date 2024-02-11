using System.Collections;
using TMPro;
using UnityEngine;
using EventManagerYC;
using Assets.Scripts.UI_related.Misc;
using JetBrains.Annotations;

namespace Assets.Scripts.Scripts_for_level_selection
{
    public class TimeSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dayText;
        [SerializeField] private TextMeshProUGUI difficultyText;

        [SerializeField]private AddTimeBehaviour addingTimeText;
        
        private static int previousTime = 0;
        private static TimeDifficulty previousDifficulty = TimeDifficulty.Easy;

        private void Start()
        {
            SetUp();
            EventManager.Instance.AddListener(TypeOfEvent.ReloadUI, SetUp);
        }

        private void SetUp()
        {
            var time = GameManager.Instance.time;
            print($"previous time{previousTime} current time {time}");
            if (time != previousTime)
            {
                print("time is different");
                SetDayText(previousTime);
                DoAddingOfTimeToText(time);
            }
            else
            {
                SetDayText(previousTime);
            }
            //Deciding the the current time difficulty
            TimeDifficulty currentDifficulty = HandleTimeDifficulty(time);

            if (currentDifficulty != previousDifficulty)
            {
                //show the changes here
            }
        }

        private void DoAddingOfTimeToText(int time)
        {
            int amountOfDayAdded = time - previousTime;

            addingTimeText.PlayAnimation(amountOfDayAdded);
            //after the animation is done playing then it will start playing the coroutine;
        }

        private TimeDifficulty HandleTimeDifficulty(int time)
        {
            TimeDifficulty currentDifficulty;
            if (time >= (int)TimeDifficulty.Hard)
            {
                currentDifficulty = TimeDifficulty.Hard;
                SetUpDifficultyText(TimeDifficulty.Hard);
            }
            else if (time >= (int)TimeDifficulty.Medium)
            {
                currentDifficulty = TimeDifficulty.Medium;
                SetUpDifficultyText(TimeDifficulty.Medium);
            }
            else
            {
                currentDifficulty = TimeDifficulty.Easy;
                SetUpDifficultyText(TimeDifficulty.Easy);
            }

            return currentDifficulty;
        }

        private void SetUpDifficultyText(TimeDifficulty difficulty)
        {
            string text = "Difficulty: ";
            switch(difficulty)
            {
                case TimeDifficulty.Hard:
                    text += "<color=red>Hard";
                    break;
                case TimeDifficulty.Medium:
                    text += "<color=orange>Medium";
                    break;
                case TimeDifficulty.Easy:
                    text += "<color=#56B847>Easy";
                    break;
            }
            difficultyText.text = text;
        }
        
        public void ReflectTimeChanges()
        {
            StartCoroutine(StartReflecting());
        }

        private IEnumerator StartReflecting()
        {
            int currentTime = GameManager.Instance.time;
            int amountChange = currentTime - previousTime;
            for(int i = 1; i <= amountChange; i++)
            {
                SetDayText(previousTime + i);
                yield return new WaitForSeconds(0.2f);
            }
            previousTime = currentTime;
        }


        private void SetDayText(int time)
        {
            dayText.text = $"{time} Day";

        }

    }

    public enum TimeDifficulty
    {
        Easy ,
        Medium = 15,
        Hard =25
    }

}