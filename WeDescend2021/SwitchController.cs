using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject lever;
    public Animator anim;

    // Sets the animation state names for the lever.
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
        // Checks for user's location, input and the current animation state.
        // Also starts the stageGenerator for the next level.
        if (Input.GetKeyDown(KeyCode.E) && state == State.idle && leverAccessible == true) {
            state = State.turnLeft;
            yield return new WaitForSeconds(0.6f);
            if (GameObject.FindGameObjectsWithTag("enemy").Length <= 0)
            {
                newLevel.instance.StartCoroutine("stageGen");
            }
            state = State.idle;
        }
        
        // Checks for user's location, input and the current animation state.
        // Also refreshes the scene to load up the next playable level.
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

    // Checks if player is in range of a collider.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            leverAccessible = true;
        }
    }
    
    // Checks if player isn't in range of a collider.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            leverAccessible = false;
        }
    }
}
