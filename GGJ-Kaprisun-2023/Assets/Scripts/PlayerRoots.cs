using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public bool nearRoot;

    public GameManager gameManager;
    public PlayerController playerController;
    public MeshRenderer meshRenderer;
    public Transform RespawnAnchor;
    public Transform LocalRespawnAnchor;
    public Object rootPrefab;
    public Transform rootOffset;
    public GameObject rootObject;

    public InputMap input;
    public InputAction Root;

    private void Awake()
    {
        input = new InputMap();
        input.Enable();
        Root = input.Map.Root;
        Root.Enable();

        Root.performed += OnRoot;
    }

    private void OnRoot(InputAction.CallbackContext context)
    {
        if (!nearRoot)
        {
            root = rootMax;
        }
    }
    void FixedUpdate()
    {
        if (!nearRoot)
        {
            if (playerController.speed < playerController.originalMaxSpeed * rootPlayerSpeedFraction && root <= rootMax && rooted == false)
            {
                root += rootFillRate * Time.deltaTime;
            }
            if (playerController.speed > playerController.originalMaxSpeed * rootPlayerSpeedFraction && root > 0 && rooted == false)
            {
                root -= rootDrainRate * Time.deltaTime;
                if (root < 0) { root = 0; }
            }
            if (root >= rootMax && rooted == false)
            {
                rooted = true;
                root = rootMax;
                TakeRoot();
            }
        }

        //Needs to look at speed not the input but the percentage of the current speed based off the max speed. 
        if(playerController.move.magnitude > rootPlayerSpeedFraction && rooted == true)
        {
            root -= rootMax / 2;
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
        playerController.speed = playerController.initialMaxSpeed - playerController.initialMaxSpeed * (root/(rootMax+1));
    }

    private void TakeRoot()
    {
        rootObject = (GameObject)PrefabUtility.InstantiatePrefab(rootPrefab);
        rootObject.transform.position = transform.position;
        rootObject.transform.rotation = transform.rotation;

        if (terrainBad == true)
        {
            sporeCount--;
            StartCoroutine(Respawn());
        }
        else
        {
            if (terrainGood == true)
            {
                RespawnAnchor.transform.position = rootOffset.transform.position;
                RespawnAnchor.transform.rotation = transform.rotation;
                sporeCount++;
                StartCoroutine(Respawn());
            }
            else
            {
                LocalRespawnAnchor.transform.position = rootOffset.transform.position;
                LocalRespawnAnchor.transform.rotation = transform.rotation;
                StartCoroutine(LocalRespawn());
            }
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
        terrainBad = false;
        terrainGood = false;
        yield return new WaitForSeconds(respawnDelay);
        respawning = true;
        transform.localScale = Vector3.zero;
        transform.position = RespawnAnchor.transform.position;
        playerController.initialMaxSpeed = playerController.originalMaxSpeed;
        meshRenderer.enabled = true;
    }
    private IEnumerator LocalRespawn()
    {
        meshRenderer.enabled = false;
        playerController.enabled = false;
        terrainBad = false;
        terrainGood = false;
        yield return new WaitForSeconds(respawnDelay);
        respawning = true;
        transform.localScale = Vector3.zero;
        transform.position = LocalRespawnAnchor.transform.position;
        playerController.initialMaxSpeed = playerController.originalMaxSpeed;
        meshRenderer.enabled = true;
    }
}
