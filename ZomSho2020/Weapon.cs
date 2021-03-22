using System.Collections;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public GameObject gunShot;
    public GameObject bulletPrefab;
    private PlayerController playerController;

    public int maxAmmo = 49;
    public int currentAmmo;
    public int startingAmmo = 0;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();

        currentAmmo = startingAmmo;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && playerController.GetGunState() == 1 && currentAmmo > 0)
        {
            Shoot();
            playerController.CreateGunShotParticle();

            currentAmmo--;
        }

        if (Input.GetButtonDown("Fire2") && playerController.GetSMGState() == 1 && currentAmmo > 0)
        {
            ShootSMG();
        }

        if (currentAmmo < 0)
        {
            currentAmmo = 0;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, gunShot.transform.position, gunShot.transform.rotation);
        FindObjectOfType<AudioManager>().Play("Shoot");
    }

    public void ShootSMG()
    {
        StartCoroutine("ShootTimerSMG");
    }

    public IEnumerator ShootTimerSMG()
    {
        if (currentAmmo >= 3)
        {
            Shoot();
            playerController.CreateGunShotParticle();
            currentAmmo--;
            yield return new WaitForSeconds(0.15f);

            Shoot();
            playerController.CreateGunShotParticle();
            currentAmmo--;
            yield return new WaitForSeconds(0.15f);

            Shoot();
            playerController.CreateGunShotParticle();
            currentAmmo--;
            yield return new WaitForSeconds(0.15f);
        }
        
        else if (currentAmmo >= 2)
        {
            Shoot();
            playerController.CreateGunShotParticle();
            currentAmmo--;
            yield return new WaitForSeconds(0.15f);

            Shoot();
            playerController.CreateGunShotParticle();
            currentAmmo--;
            yield return new WaitForSeconds(0.15f);
        }

        else if (currentAmmo >= 1)
        {
            Shoot();
            playerController.CreateGunShotParticle();
            currentAmmo--;
            yield return new WaitForSeconds(0.15f);
        }
    }
}
