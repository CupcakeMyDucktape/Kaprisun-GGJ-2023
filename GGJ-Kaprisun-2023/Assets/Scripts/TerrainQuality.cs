using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainQuality : MonoBehaviour
{
    PlayerRoots playerRoots;
    public bool terrainBad;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) //On collision with player: Gets PlayerRoots and sets terrain boolean
        {
            playerRoots = other.GetComponent<PlayerRoots>();
            if (terrainBad)
            {
                playerRoots.terrainBad = true;
            }
            else
            {
                playerRoots.terrainGood = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) //On collision exit with player: Undoes any boolean changes
        {
            playerRoots.terrainBad = false; 
            playerRoots.terrainGood = false;
        }
    }
}
