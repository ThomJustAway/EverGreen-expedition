using Assets.Scripts.Scripts_for_level_selection;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI tipText;
    [SerializeField] private DifficultyMessage[] messages; 
    public void SetAndPlayDifficultyCanvas(TimeDifficulty difficulty)
    {
        var message = SearchAndGiveMessageAccordingly(difficulty);
        difficultyText.text = "<b>" + message.difficulty.ToString();
        difficultyText.color = message.color;

        tipText.text = message.message.Trim();
        SoundManager.Instance.PlayAudio(SFXClip.DifficultyReach);
        gameObject.SetActive(true);
    }

    private DifficultyMessage SearchAndGiveMessageAccordingly(TimeDifficulty difficulty)
    {
        for(int i = 0; i < messages.Length; i++)
        {
            if (messages[i].difficulty == difficulty)
            {
                return messages[i]; 
            }
        }
        Debug.LogError("Cant find difficulty");
        return messages[0];
    }

}

[Serializable] 
public struct DifficultyMessage
{
    public TimeDifficulty difficulty;
    [TextArea(3 ,5)]
    public string message;
    public Color color;
}
