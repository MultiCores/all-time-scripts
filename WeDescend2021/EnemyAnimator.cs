using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public GameObject enemy;
    public Animator anim;
    Rigidbody2D body;

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
        if (alive)
        {
            AnimationState();
        }

        anim.SetInteger("state", (int)state);
    }

    private void AnimationState()
    {
        if(enemyControl.pf.canMove == true)
        {
            state = State.running;
        }
        /*if(Input.GetKeyDown(KeyCode.K) && enemyPhaseTesting != true)
        {
            state = State.running;
            enemyPhaseTesting = true;

        }
        else if(Input.GetKeyDown(KeyCode.K) && enemyPhaseTesting == true)
        {
            state = State.idle;
            enemyPhaseTesting = false;
        }*/
        else
        {
            state = State.idle;
        }
    }
}
