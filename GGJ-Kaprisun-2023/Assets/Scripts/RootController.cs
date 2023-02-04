using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
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
    }
}
