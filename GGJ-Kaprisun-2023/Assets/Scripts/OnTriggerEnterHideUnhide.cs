using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterHideUnhide : MonoBehaviour
{
    public bool setActive;
    [SerializeField] public GameObject[] targets;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // loop through the array and set them active/inactive
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].SetActive(setActive);
            }
        }
    }
}
