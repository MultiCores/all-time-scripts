using UnityEngine;

public class Zombie : MonoBehaviour
{

    [SerializeField] private float leftCap = -1;
    [SerializeField] private float rightCap = -1;

    [SerializeField] private float movementLeft = 0f;
    [SerializeField] private float movementRight = 0f;

    private int shotCount = 0;
    private int walkTimer1 = -1;

    private Rigidbody2D Rigidbody;
    private Animator Anim;
    public GameObject Player;

    private bool facingLeft = false;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }
    private void Update()
    {
        Range();
    }

    private void LateUpdate()
    {
        if (Rigidbody.velocity.x > 0.01 || Rigidbody.velocity.x < -0.01)
        {
            Anim.SetBool("Walking", true);
        }

        else
        {
            Anim.SetBool("Walking", false);
        }
    }

    void Range()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);

        if (distance <= 4.5)
        {
            if (transform.position.x > Player.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                Anim.SetBool("Walking", true);
            }
            else if (transform.position.x < Player.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                Anim.SetBool("Walking", true);
            }
            
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, .007f);
        }
    }

    public void Move()
    {
        if (facingLeft)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

            if (transform.position.x > leftCap)
            {
                
                for (walkTimer1 = 0; walkTimer1 <= 8; walkTimer1++)
                {
                    Rigidbody.velocity = new Vector2(-movementLeft, Rigidbody.velocity.y);
                }

                if (walkTimer1 >= 8)
                {
                    walkTimer1 = 0;
                }
            }

            else
            {
                facingLeft = false;
            }
        }

        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

            if (transform.position.x < rightCap)
            {
                
                for (walkTimer1 = 0; walkTimer1 <= 8; walkTimer1++)
                {
                    Rigidbody.velocity = new Vector2(movementRight, Rigidbody.velocity.y);
                }

                if (walkTimer1 >= 8)
                {
                    walkTimer1 = 0;
                }
            }

            else
            {
                facingLeft = true;
            }
        }
    }

    public void Shot()
    {
        if (shotCount < 1)
        {
            Anim.SetTrigger("Death");
            GetComponent<BoxCollider2D>().enabled = false;
            shotCount += 1;
        }
        
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}