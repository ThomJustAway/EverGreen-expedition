using Patterns;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUp : Singleton<PopUp>
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI popUpText;
    private bool isUsed;

    public void ShowMessage(string message , int duration)
    {
        if (!isUsed)
        {
            isUsed = true;
            popUpText.text = message.Trim();
            StartCoroutine(CoroutineForPopUp(duration));
        }

        //else ignore the message for now.
    }
    
    private IEnumerator CoroutineForPopUp(int duration)
    {
        animator.SetBool("Activated" , true);
        yield return new WaitForSeconds(duration);
        animator.SetBool("Activated", false);
        isUsed = false;
    }

}
