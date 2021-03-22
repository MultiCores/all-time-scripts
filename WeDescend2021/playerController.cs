using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D body;
    Animator anim;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float speed = 20.0f;
    public int rng;
    public soundManager soundManager;

    Vector3 localscale;
    public bool facingLeft;

    // State machine's states
    private enum State {idle, running}
    private State state = State.idle;
    bool alive = true;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localscale = transform.localScale;

        StartCoroutine(NumberGen());
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x && facingLeft == false || Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x && facingLeft == true)
        {
            localscale.x *= -1;
            facingLeft = !facingLeft;
        }

        transform.localScale = localscale;

        if(alive)
        {
            AnimationState();
        }

        anim.SetInteger("state", (int) state);
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
    IEnumerator NumberGen()
    {
        while (true)
        {
                rng = Random.Range(1, 9);
                yield return new WaitForSeconds(0.3f);
        }
    }

    private void AnimationState()
    {
        if(Mathf.Abs(body.velocity.x) > 2f || Mathf.Abs(body.velocity.y) > 2f)
        {
            state = State.running;

            switch (rng)
            {
                case 0:
                    break;
                case 1:
                    soundManager.playSound("footstep_gravel01");
                    //rng = 0;
                    break;
                case 2:
                    soundManager.playSound("footstep_gravel02");
                    //rng = 0;
                    break;
                case 3:
                    soundManager.playSound("footstep_gravel03");
                    //rng = 0;
                    break;
                case 4:
                    soundManager.playSound("footstep_gravel04");
                    //rng = 0;
                    break;
                case 5:
                    soundManager.playSound("footstep_gravel05");
                    //rng = 0;
                    break;
                case 6:
                    soundManager.playSound("footstep_stone01");
                    //rng = 0;
                    break;
                case 7:
                    soundManager.playSound("footstep_stone02");
                    //rng = 0;
                    break;
                case 8:
                    soundManager.playSound("footstep_stone03");
                    //rng = 0;
                    break;
                case 9:
                    soundManager.playSound("footstep_stone04");
                    //rng = 0;
                    break;
            }
        }

        else
        {
            state = State.idle;
        }
    }
}
