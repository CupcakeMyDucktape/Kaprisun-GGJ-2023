using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floppy : MonoBehaviour
{
    public Rigidbody2D PlayerRB;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(new Vector3 (0, 1, PlayerRB.velocity.x));
    }
}
