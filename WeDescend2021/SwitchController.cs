using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject lever;
    public Animator anim;

    private enum State {idle, turnLeft, turnRight}
    private State state = State.idle;

    bool leverAccessible = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        StartCoroutine(AnimationState());
        anim.SetInteger("state", (int)state);
    }

    IEnumerator AnimationState()
    {
        if (Input.GetKeyDown(KeyCode.E) && state == State.idle && leverAccessible == true) {
            state = State.turnLeft;
            yield return new WaitForSeconds(0.6f);
            if (GameObject.FindGameObjectsWithTag("enemy").Length <= 0)
            {
                newLevel.instance.StartCoroutine("stageGen");
            }
            state = State.idle;
        }

        else if (Input.GetKeyDown(KeyCode.Q) && state == State.idle) {
            state = State.turnRight;
            yield return new WaitForSeconds(0.6f);
            if (GameObject.FindGameObjectsWithTag("enemy").Length <= 0)
            {
                SceneManager.LoadScene("Game");
            }

            state = State.idle;
        }
        yield break;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            leverAccessible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            leverAccessible = false;
        }
    }
}
