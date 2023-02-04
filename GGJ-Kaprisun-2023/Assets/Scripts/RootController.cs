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
        if (other.gameObject.CompareTag("Dirt"))
        {
            other.gameObject.tag = "Rooted Dirt";
        }
        if (other.gameObject.CompareTag("Rock"))
        {
            other.gameObject.tag = "Rooted Dirt";
            other.enabled = false;
        }
        if(other.gameObject.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.initialMaxSpeed *= speedMultiplier;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController.initialMaxSpeed /= speedMultiplier;
        }
    }
}
