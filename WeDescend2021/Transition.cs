using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Animator transition;
    public soundManager soundManager;

    public void StartAnimation()
    {
        StartCoroutine(transitionAnimationTrigger());
    }

    IEnumerator transitionAnimationTrigger()
    {
        soundManager.playSound("transition01d");

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(0.8f);

        transition.SetTrigger("GoBack");
    }
}
