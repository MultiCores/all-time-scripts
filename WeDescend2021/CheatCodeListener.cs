using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeListener : MonoBehaviour
{
    private string[] cheatCode;
    private int index;
    public soundManager soundManager;

    private int rng;

    public GameObject SpawnableEnemy1;

    void Start()
    {
        StartCoroutine(NumberGen());
		
		// Checks for the word, that will be expected from the user.
        cheatCode = new string[] { "t","e","s","t" };
        index = 0;
    }

    void Update()
    {
		// Checks for user input.
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCode[index]))
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
        if (index == cheatCode.Length)
        {
			// Spawns an entity to set coordinate.
            Instantiate(SpawnableEnemy1, new Vector3(0, 0, 0), Quaternion.identity);
            index = 0;
	
			// Plays a sound determined by an rng number.
            switch (rng)
            {
                case 0:
                    soundManager.playSound("test1");
                    break;
                case 1:
                    soundManager.playSound("test2");
                    break;
                case 2:
                    soundManager.playSound("test3");
                    break;
                case 3:
                    soundManager.playSound("test4");
                    break;
                case 4:
                    soundManager.playSound("test5");
                    break;
                case 5:
                    soundManager.playSound("test6");
                    break;
            }
        }
    }

	// Creates a number determined by rng after a certain period of time.
    IEnumerator NumberGen()
    {
        while (true)
        {
            rng = Random.Range(0, 5);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
