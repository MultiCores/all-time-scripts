using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Collider2D collision;
    [SerializeField] private PlayerController player;
    private void Start()
    {
        levelManager.GetComponent<LevelManager>();
        collision.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (player.GetCurrentHealth() <= 0)
        {
            levelManager.respawn();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelManager.respawn();
            player.currentHealth = 3;
        }
    }
}
