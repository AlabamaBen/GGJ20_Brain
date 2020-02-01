﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room4_Behaviour : MonoBehaviour
{

    //public Collider2D Door_Locker;

    public Collider2D Crystal_01;
    public Collider2D Crystal_02;
    public Collider2D Crystal_03;
    public Collider2D Crystal_04;

    public GameObject Door;

    private GameObject Player;

    private bool Doorlocked = false;

    public float Color_Lerp_Speed = 3f;

    private bool Crystal_Cooldown; 

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Crystal_Cooldown = false; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        /*
        if (!Doorlocked && Door_Locker.OverlapPoint(Player.transform.position))
        {
            Door.SetActive(true);
            Doorlocked = true;
        }
        */

        if(!Crystal_Cooldown)
        {
            if(Crystal_01.OverlapPoint(Player.transform.position))
            {
                Crystal_Cooldown = true;
                Invoke("Reset_Crystal_Cooldown", 2f);
                Crystal_01.GetComponent<Crystal_Behviour>().SetActive(true);
            }
            if (Crystal_02.OverlapPoint(Player.transform.position))
            {
                Crystal_Cooldown = true;
                Invoke("Reset_Crystal_Cooldown", 2f);
                Crystal_02.GetComponent<Crystal_Behviour>().SetActive(true);
            }
            if (Crystal_03.OverlapPoint(Player.transform.position))
            {
                Crystal_Cooldown = true;
                Invoke("Reset_Crystal_Cooldown", 2f);
                Crystal_03.GetComponent<Crystal_Behviour>().SetActive(true);
            }
            if (Crystal_04.OverlapPoint(Player.transform.position))
            {
                Crystal_Cooldown = true;
                Invoke("Reset_Crystal_Cooldown", 2f);
                Crystal_04.GetComponent<Crystal_Behviour>().SetActive(true);
            }
        }

    }

    private void Reset_Crystal_Cooldown()
    {
        Crystal_Cooldown = false;
    }
}
