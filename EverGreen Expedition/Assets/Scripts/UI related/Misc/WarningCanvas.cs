using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningCanvas : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayWarning()
    {
        SoundManager.Instance.PlayAudio(SFXClip.WarningSFX);
        gameObject.SetActive(true);

        //do coroutine
        StartCoroutine(StartCountDown());
    }


    private IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(4f);
        animator.SetTrigger("CanExit");
    }

    //called during the animation itself
    public void EndAnimation()
    {
        gameObject.SetActive(false);
    }
}
