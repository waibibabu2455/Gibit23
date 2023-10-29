using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

    private GameObject Player;

    public Image Bar;

    public float lerpSpeed = 3;

    public float maxHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player == null){
            Player = GameObject.FindWithTag("Player");
        }
        BarFiller();
    }

    private void BarFiller()
    {
        
        if(Player != null)
        {
            
            float health = Player.GetComponent<PlayerController>().health;
            Debug.Log("Current player health is");
            Debug.Log(health);
            
            //Bar.fillAmount = health / maxHealth;
            Bar.fillAmount = Mathf.Lerp(Bar.fillAmount,  health / maxHealth, lerpSpeed*Time.deltaTime);
        }
    }

}
