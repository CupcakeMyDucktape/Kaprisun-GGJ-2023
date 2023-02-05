using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public PlayerController player;
    public PlayerRoots roots;
   // public SpeedModifier speedMod;
    public AudioSource walk;
    public AudioSource uproot;
    public AudioSource sizzle;
    public bool isSlow;
    public bool touchLight;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        roots = FindObjectOfType<PlayerRoots>();
        //speedMod = FindObjectOfType<SpeedModifier>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.speed <= 0f)
        {
            walk.volume = 0f;
        }
        else
        {
            walk.volume = 0.2f;
        }
        if (roots.root >= 10)
        {
            isSlow = true;
        }
        if (isSlow && roots.root < 10)
        {
            isSlow = false;
            uproot.Play();
        }
        //if (speedMod.speedMultiplier <= 1)
        //{
        //    touchLight = true;
        //    sizzle.Play();
        //}
        //if (speedMod.speedMultiplier > 1)
        //{
        //    touchLight = false;
        //}
    }
}
