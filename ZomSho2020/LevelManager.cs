using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject respawnPoint;
    public PlayerController player;

    public GameObject checkPoint1;
    public GameObject checkPoint2;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void respawn()
    {
        StartCoroutine("playerRespawn");
    }

    public IEnumerator playerRespawn()
    {
        FindObjectOfType<AudioManager>().Play("Death");
        player.enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(1);

        player.transform.position = respawnPoint.transform.position;
        player.currentHealth = 3;
        player.enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
    }
}
