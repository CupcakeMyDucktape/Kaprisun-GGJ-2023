using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    public float speedMultiplier;
    PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D other)
    {

        print(other.gameObject.tag);
        if (other.gameObject.CompareTag("Dirt")) //On collision with dirt: Changes tag to Rooted Dirt
        {
            other.gameObject.tag = "Rooted Dirt";
        }
        if (other.gameObject.CompareTag("Rock")) //On collision with rocks: Changes tag to Rooted Dirt and disables Collider2D
        {
            other.gameObject.tag = "Rooted Dirt";
            other.enabled = false;
        }
        if(other.gameObject.CompareTag("Player")) //On collision with player: Gets PlayerController and applies speed multiplier
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.initialMaxSpeed *= speedMultiplier;
            if(playerController.initialMaxSpeed > 450)
            {
                playerController.initialMaxSpeed = 450;
            }
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
