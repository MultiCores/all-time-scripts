using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D PlayerRigidbody;
    private Animator Anim;
    private Collider2D Collider;
    private Weapon weapon;
    private LevelLoader levelLoader;
    private LevelManager levelManager;

    private enum State {idle, running, jumping, falling, hurt, idleGun, runningGun, jumpingGun, hurtGun, fallingGun, idleSMG, runningSMG, jumpingSMG, hurtSMG, fallingSMG}
    private State state = State.idle;
    
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 4.7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float hurtForce = 2.2f;
    [SerializeField] private int pistolEquipped = 0;
    [SerializeField] private int smgEquipped = 0;
    [SerializeField] private Text ammoCount;

    public ParticleSystem dust;
    public ParticleSystem gunshotParticle;

    public int numOfHps;
    public Image[] hps;
    public Sprite fullHp;
    public Sprite emptyHp;
    public int currentHealth;

    private void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Collider = GetComponent<Collider2D>();
        weapon = GetComponent<Weapon>();
        levelLoader = GetComponent<LevelLoader>();
        levelManager = FindObjectOfType<LevelManager>();

        currentHealth = 3;
    }

    private void Update()
    {
        if (state != State.hurt && pistolEquipped == 0 && smgEquipped == 0)
        {
            Movement();
        }

        if (state != State.hurtGun && pistolEquipped == 1)
        {
            MovementGun();
        }

        if (state != State.hurtSMG && smgEquipped == 1) 
        {
            MovementSMG();
        }

        if (pistolEquipped == 0 && smgEquipped == 0)
        {
            AnimationState();
        }

        if (pistolEquipped == 1)
        {
            AnimationStatePistol();
        }

        if (smgEquipped == 1)
        {
            AnimationStateSMG();
        }

        Anim.SetInteger("state", (int) state); // Sets anim based on Enumerator state.
        ammoCount.text = weapon.currentAmmo + "";


        if (currentHealth > numOfHps)
        {
            currentHealth = numOfHps; 
        }

        for (int i = 0; i < hps.Length; i++)
        {
            if (i < currentHealth)
            {
                hps[i].sprite = fullHp;
            }
            else
            {
                hps[i].sprite = emptyHp;
            }

            if (i < numOfHps)
            {
                hps[i].enabled = true;
            } 
            else
            {
                hps[i].enabled = false;
            }
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            FindObjectOfType<AudioManager>().Play("Pickup");
            Destroy(collision.gameObject);
            weapon.currentAmmo += 7;

            if (weapon.currentAmmo > weapon.maxAmmo)
            {
                weapon.currentAmmo = weapon.maxAmmo;
            }
        }

        if (collision.tag == "HealthKit")
        {
            FindObjectOfType<AudioManager>().Play("Pickup");
            Destroy(collision.gameObject);
            currentHealth++;

            if (currentHealth > numOfHps)
            {
                currentHealth = numOfHps;
            }
        }

        if (collision.tag == "PistolSpawn")
        {
            FindObjectOfType<AudioManager>().Play("Pickup");
            Destroy(collision.gameObject);
            pistolEquipped = 1;

            if (smgEquipped == 1)
            {
                smgEquipped = 0;
            }
        }

        if (collision.tag == "SMGSpawn")
        {
            FindObjectOfType<AudioManager>().Play("Pickup");
            Destroy(collision.gameObject);
            smgEquipped = 1;

            if (pistolEquipped == 1)
            {
                pistolEquipped = 0;
            }
        }

        if (collision.tag == "CheckpointTrigger1")
        {
            levelManager.respawnPoint.transform.position = levelManager.checkPoint1.transform.position;
        }

        if (collision.tag == "CheckpointTrigger2")
        {
            levelManager.respawnPoint.transform.position = levelManager.checkPoint2.transform.position;
        }

        if (collision.tag == "Finish")
        {
            levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
            levelLoader.LoadNextLevel();
        }

        if (collision.tag == "FinishMainMenu")
        {
            levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
            levelLoader.LoadMainMenu();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && pistolEquipped == 0 && smgEquipped == 0)
        {
            
            state = State.hurt;
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                // Enemy is to my right, I should be moved left
                PlayerRigidbody.velocity = new Vector2(-hurtForce, PlayerRigidbody.velocity.y);
                FindObjectOfType<AudioManager>().Play("Hurt");
                CreateDust();
                currentHealth--;
             }
             else
             {
                // Enemy is to my left, I should be moved right
                PlayerRigidbody.velocity = new Vector2(hurtForce, PlayerRigidbody.velocity.y);
                FindObjectOfType<AudioManager>().Play("Hurt");
                CreateDust();
                currentHealth--;
            }
        }

        else if (other.gameObject.tag == "Enemy" && pistolEquipped == 1)
        {

            state = State.hurtGun;
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                // Enemy is to my right, I should be moved left
                PlayerRigidbody.velocity = new Vector2(-hurtForce, PlayerRigidbody.velocity.y);
                FindObjectOfType<AudioManager>().Play("Hurt");
                CreateDust();
                currentHealth--;
            }
            else
            {
                // Enemy is to my left, I should be moved right
                PlayerRigidbody.velocity = new Vector2(hurtForce, PlayerRigidbody.velocity.y);
                FindObjectOfType<AudioManager>().Play("Hurt");
                CreateDust();
                currentHealth--;
            }
        }

        else if (other.gameObject.tag == "Enemy" && smgEquipped == 1)
        {

            state = State.hurtSMG;
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                // Enemy is to my right, I should be moved left
                PlayerRigidbody.velocity = new Vector2(-hurtForce, PlayerRigidbody.velocity.y);
                FindObjectOfType<AudioManager>().Play("Hurt");
                CreateDust();
                currentHealth--;
            }
            else
            {
                // Enemy is to my left, I should be moved right
                PlayerRigidbody.velocity = new Vector2(hurtForce, PlayerRigidbody.velocity.y);
                FindObjectOfType<AudioManager>().Play("Hurt");
                CreateDust();
                currentHealth--;
            }
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        // Moving left.
        if (hDirection < 0)
        {
            CreateDust();
            PlayerRigidbody.velocity = new Vector2(-speed, PlayerRigidbody.velocity.y);
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // Moving right.
        else if (hDirection > 0)
        {
            CreateDust();
            PlayerRigidbody.velocity = new Vector2(speed, PlayerRigidbody.velocity.y);
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Jumping.
        if (Input.GetButtonDown("Jump") && Collider.IsTouchingLayers(ground))
        {
            PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, jumpForce);
            FindObjectOfType<AudioManager>().Play("Jump");
            state = State.jumping;
        }
    }

    private void MovementGun()
    {
        float hDirection = Input.GetAxis("Horizontal");

        // Moving left.
        if (hDirection < 0)
        {
            CreateDust();
            PlayerRigidbody.velocity = new Vector2(-speed, PlayerRigidbody.velocity.y);
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // Moving right.
        else if (hDirection > 0)
        {
            CreateDust();
            PlayerRigidbody.velocity = new Vector2(speed, PlayerRigidbody.velocity.y);
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Jumping.
        if (Input.GetButtonDown("Jump") && Collider.IsTouchingLayers(ground))
        {
            PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, jumpForce);
            FindObjectOfType<AudioManager>().Play("Jump");
            state = State.jumpingGun;
        }
    }

    private void MovementSMG()
    {
        float hDirection = Input.GetAxis("Horizontal");

        // Moving left.
        if (hDirection < 0)
        {
            CreateDust();
            PlayerRigidbody.velocity = new Vector2(-speed, PlayerRigidbody.velocity.y);
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // Moving right.
        else if (hDirection > 0)
        {
            CreateDust();
            PlayerRigidbody.velocity = new Vector2(speed, PlayerRigidbody.velocity.y);
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Jumping.
        if (Input.GetButtonDown("Jump") && Collider.IsTouchingLayers(ground))
        {
            PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, jumpForce);
            FindObjectOfType<AudioManager>().Play("Jump");
            state = State.jumpingSMG;
        }
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (PlayerRigidbody.velocity.y < .1f)
            {
                state = State.falling;
            }
        }

        else if (state == State.falling)
        {
            if (Collider.IsTouchingLayers(ground))
            {
                CreateDust();
                state = State.idle;
            }
        }

        else if (state == State.hurt)
        {
            if (Mathf.Abs(PlayerRigidbody.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(PlayerRigidbody.velocity.x) > 2f)
        {
            // Moving
            state = State.running;
        }

        else
        {
            state = State.idle;
        }
    }

    private void AnimationStatePistol()
    {
        if (state == State.jumpingGun)
        {
            if (PlayerRigidbody.velocity.y < .1f)
            {
                state = State.fallingGun;
            }
        }

        else if (state == State.fallingGun)
        {
            if (Collider.IsTouchingLayers(ground))
            {
                CreateDust();
                state = State.idleGun;
            }
        }

        else if (state == State.hurtGun)
        {
            if (Mathf.Abs(PlayerRigidbody.velocity.x) < .1f)
            {
                state = State.idleGun;
            }
        }

        else if (Mathf.Abs(PlayerRigidbody.velocity.x) > 2f)
        {
            // Moving
            state = State.runningGun;
        }

        else
        {
            state = State.idleGun;
        }
    }

    private void AnimationStateSMG()
    {
        if (state == State.jumpingSMG)
        {
            if (PlayerRigidbody.velocity.y < .1f)
            {
                state = State.fallingSMG;
            }
        }

        else if (state == State.fallingSMG)
        {
            if (Collider.IsTouchingLayers(ground))
            {
                CreateDust();
                state = State.idleSMG;
            }
        }

        else if (state == State.hurtSMG)
        {
            if (Mathf.Abs(PlayerRigidbody.velocity.x) < .1f)
            {
                state = State.idleSMG;
            }
        }

        else if (Mathf.Abs(PlayerRigidbody.velocity.x) > 2f)
        {
            // Moving
            state = State.runningSMG;
        }

        else
        {
            state = State.idleSMG;
        }
    }

    public int GetSMGState()
    {
        return smgEquipped;
    }

    public int GetGunState()
    {
        return pistolEquipped;
    }

    void CreateDust()
    {
        dust.Play();
    }

    public void CreateGunShotParticle()
    {
        gunshotParticle.Play();
    }
}