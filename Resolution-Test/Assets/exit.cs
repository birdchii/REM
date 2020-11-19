using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour
{
    public GameObject exitButton;


    public void ExitDialogue()
    {
        exitButton.SetActive(false);

        if (exitButton != null)
        {
            Destroy(this.gameObject);

        }

    }
}
