using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    public AudioSource menuButton;
    public void ClickSound()
    {
        menuButton.Play();
    }
}
