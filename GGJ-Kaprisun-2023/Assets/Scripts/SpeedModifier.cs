using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifier : MonoBehaviour
{
    PlayerController playerController;
    public float speedMultiplier;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) //On collision with player: Gets PlayerController and applies speed multiplier
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.initialMaxSpeed *= speedMultiplier;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) //On collision exit with player: Undoes speed multiplier
        {
            playerController.initialMaxSpeed /= speedMultiplier;
        }
    }
}
