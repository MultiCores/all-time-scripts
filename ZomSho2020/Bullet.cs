using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D bulletRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidBody.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Zombie zombie = hitInfo.gameObject.GetComponent<Zombie>();
        if (zombie != null)
        {
            zombie.Shot();
            Debug.Log(hitInfo);
        }
        Destroy(gameObject);
    }
}
