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
    public int sporeCount;

    public Vector3 rootPosition;
    public float respawnDelay;
    private Vector3 respawnScaleAccel;
    public bool respawning;
    public bool terrainBad;
    public bool terrainGood;

    public GameManager gameManager;
    public PlayerController playerController;
    public MeshRenderer meshRenderer;
    public Transform RespawnAnchor;
    public Transform LocalRespawnAnchor;
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

        //Needs to look at speed not the input but the percentage of the current speed based off the max speed. 
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
        rootObject = (GameObject)PrefabUtility.InstantiatePrefab(rootPrefab);
        rootObject.transform.position = transform.position;
        rootObject.transform.rotation = transform.rotation;

        if (terrainBad == true)
        {
            LocalRespawnAnchor.position = rootOffset.transform.position;
            LocalRespawnAnchor.rotation = transform.rotation;
            sporeCount--;
            StartCoroutine(Respawn());
        }
        if(terrainGood == true)
        {
            RespawnAnchor.transform.position = rootOffset.transform.position;
            RespawnAnchor.transform.rotation = transform.rotation;
            sporeCount++;
            StartCoroutine(Respawn());
        }
        else
        {
            RespawnAnchor.transform.position = rootOffset.transform.position;
            RespawnAnchor.transform.rotation = transform.rotation;
            StartCoroutine(Respawn());
        }
        if(sporeCount < 0)
        {
            gameManager.GameOver();
        }
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
    private IEnumerator LocalRespawn()
    {
        meshRenderer.enabled = false;
        playerController.enabled = false;
        yield return new WaitForSeconds(respawnDelay);
        respawning = true;
        transform.localScale = Vector3.zero;
        transform.position = LocalRespawnAnchor.transform.position;
        meshRenderer.enabled = true;
    }
}
