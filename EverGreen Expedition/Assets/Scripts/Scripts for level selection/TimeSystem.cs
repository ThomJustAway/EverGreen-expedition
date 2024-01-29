using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Scripts_for_level_selection
{
    public class TimeSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dayText;
        [SerializeField] private TextMeshProUGUI difficultyText;
        private void Start()
        {
            SetUp();
            EventManager.Instance.AddListener(TypeOfEvent.ReloadUI, SetUp);
        }

        private void SetUp()
        {
            var time = GameManager.Instance.time;
            if (time >= (int)TimeDifficulty.Hard)
            {
                SetUpDifficultyText(TimeDifficulty.Hard);
            }
            else if(time >= (int)TimeDifficulty.Medium)
            {
                SetUpDifficultyText(TimeDifficulty.Medium);
            }
            else
            {
                SetUpDifficultyText(TimeDifficulty.Easy);
            }
            SetUpDayText(time);
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

        private void SetUpDayText(int time)
        {
            string text = $"{time} Day";
            dayText.text = text;
        }
    }

    public enum TimeDifficulty
    {
        Easy ,
        Medium = 15,
        Hard =25
    }

}