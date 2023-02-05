using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swoop : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;

    void Update()
    {
        anim.SetFloat("x Velocity",-rb.velocity.x);
    }
}
