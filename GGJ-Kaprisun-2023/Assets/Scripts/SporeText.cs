using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SporeText : MonoBehaviour
{
    public PlayerRoots player;
    public TMP_Text text;
    private void Start()
    {
        player = FindObjectOfType<PlayerRoots>();
    }
    private void Update()
    {
        text.text = "Spores:" + player.root;
    }
}
