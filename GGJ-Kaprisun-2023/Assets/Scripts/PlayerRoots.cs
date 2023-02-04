using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerRoots : MonoBehaviour
{

    public float rootPlayerSpeedFraction;
    public float root;
    public float rootMax;
    public float rootFillRate;
    public float rootDrainRate;
    public bool rooted;

    public Vector3 rootPosition;

    public PlayerController playerController;
    public Object rootPrefab;
    public Transform rootOffset;
    public GameObject rootObject;

    void FixedUpdate()
    {
        if(playerController.move.magnitude < rootPlayerSpeedFraction && root <= rootMax) 
        {
            root += rootFillRate * Time.deltaTime;
        }
        if(playerController.move.magnitude > rootPlayerSpeedFraction && root > 0)
        {
            root -= rootDrainRate * Time.deltaTime;
            if(root < 0) { root = 0; }
        }
        if(root >= rootMax && rooted == false)
        {
            rooted = true;
            root = rootMax;
            TakeRoot();
        }
        if (playerController.move.magnitude > rootPlayerSpeedFraction && rooted == true)
        {
            root -= rootDrainRate * Time.deltaTime;
            rooted = false;
        }
        playerController.speed = playerController.initialMaxSpeed - playerController.initialMaxSpeed * (root/rootMax);
    }

    private void TakeRoot()
    {
        Destroy(rootObject);
        rootObject = (GameObject)PrefabUtility.InstantiatePrefab(rootPrefab);
        rootObject.transform.position = rootOffset.position;
    }
}
