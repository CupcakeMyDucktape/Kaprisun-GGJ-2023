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
    public float respawnDelay;
    private Vector3 respawnScaleAccel;
    public bool respawning;

    public PlayerController playerController;
    public MeshRenderer meshRenderer;
    public Transform RespawnAnchor;
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
        if(playerController.move.magnitude > rootPlayerSpeedFraction && rooted == true)
        {
            root -= rootDrainRate * Time.deltaTime;
            rooted = false;
        }
        if(respawning == true)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, Vector3.one, ref respawnScaleAccel, 0.2f);
            if(transform.localScale.magnitude > Vector3.one.magnitude * 0.98f)
            {
                playerController.enabled = true;
                respawning = false;
                transform.localScale = Vector3.one;
            }
        }
        playerController.speed = playerController.initialMaxSpeed - playerController.initialMaxSpeed * (root/rootMax);

        
    }

    private void TakeRoot()
    {
        //Leave old objects around the level 
        //Destroy(rootObject);
        //add if statement if the terrain is good reset the respawn position to the rootoffset. If the terrain is bad leave the respawn anchor
        rootObject = (GameObject)PrefabUtility.InstantiatePrefab(rootPrefab);
        rootObject.transform.position = transform.position;
        rootObject.transform.rotation = transform.rotation;

        //Edited out to use a seperate respawn anchor :) 
        //RespawnAnchor.transform.position = rootOffset.transform.position;

        RespawnAnchor.transform.rotation = transform.rotation;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        meshRenderer.enabled = false;
        playerController.enabled = false;
        yield return new WaitForSeconds(respawnDelay);
        respawning = true;
        transform.localScale = Vector3.zero;
        transform.position = RespawnAnchor.transform.position;
        meshRenderer.enabled = true;
    }
}
