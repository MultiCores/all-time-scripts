using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public GameObject enemy;
    public Animator anim;
    Rigidbody2D body;

    // All enemies animations' names set below.
    private enum State {idle, running}
    private State state = State.idle;
    bool alive = true;
    bool enemyPhaseTesting = false;

    public EnemyController enemyControl;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyControl = GetComponent<EnemyController>();
    }

    private void Update()
    {
        // Checks for whether or not player is alive. If is, then it updates the AnimationState every frame.
        if (alive)
        {
            AnimationState();
        }

        anim.SetInteger("state", (int)state);
    }

    private void AnimationState()
    {
        // Checks if player is moving, then making the 'running' animation play. If not, make the player's animation play 'idle'.
        if(enemyControl.pf.canMove == true)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
}
