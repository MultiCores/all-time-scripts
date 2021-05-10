using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPopup : MonoBehaviour
{
    public SwitchController switchController;

    public GameObject ButtonQ;
    public GameObject ButtonE;

    private void Update()
    {
        if(switchController.leverAccessible == true)
        {
            ButtonQ.SetActive(true);
            ButtonE.SetActive(true);
        }
        else
        {
            ButtonQ.SetActive(false);
            ButtonE.SetActive(false);
        }
    }
}
