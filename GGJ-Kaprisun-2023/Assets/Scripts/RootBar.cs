using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootBar : MonoBehaviour
{
    //public Image healthBarImage;
    public PlayerRoots player;
    public Image rootBar;
    private void Start()
    {
        player = FindObjectOfType<PlayerRoots>();
    }
    /*public void UpdateHealthBar()
    {
        
        healthBarImage.fillAmount = Mathf.Clamp(player.health / player.maxHealth, 0, 1f);
    }*/
    private void Update()
    {
        rootBar.fillAmount = Mathf.Clamp(player.root / player.rootMax, 0, 1f);
    }
}
