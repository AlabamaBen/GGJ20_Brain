using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room4_Behaviour : MonoBehaviour
{

    public Collider2D Door_Locker;

    public Collider2D Crystal_01;
    public Collider2D Crystal_02;
    public Collider2D Crystal_03;
    public Collider2D Crystal_04;

    public Collider2D Synapse_Trigger;


    public GameObject Door;

    private GameObject Player;

    private bool Doorlocked = false;

    public float Color_Lerp_Speed = 3f;

    private bool Crystal_Cooldown;

    private Collider2D Last_Crystal; 

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Crystal_Cooldown = false;

        Last_Crystal = null; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (Synapse_Trigger.OverlapPoint(Player.transform.position))
        {
            GameManager.Gm.ReadActivated = true;
        }


        if (!Doorlocked && Door_Locker.OverlapPoint(Player.transform.position))
        {
            Door.SetActive(true);
            Doorlocked = true;
        }

        if(!Crystal_Cooldown)
        {
            if(Crystal_01.OverlapPoint(Player.transform.position) && !Crystal_01.GetComponent<Crystal_Behviour>().IsActive)
            {
                if(Last_Crystal == null)
                {
                    Crystal_01.GetComponent<Crystal_Behviour>().SetActive(true);
                    Last_Crystal = Crystal_01;
                }
                else
                {

                }
                Crystal_Cooldown = true;
                Invoke("Reset_Crystal_Cooldown", 2f);

            }
            if (Crystal_02.OverlapPoint(Player.transform.position) && !Crystal_02.GetComponent<Crystal_Behviour>().IsActive)
            {

                if (Last_Crystal == Crystal_01)
                {
                    Crystal_02.GetComponent<Crystal_Behviour>().SetActive(true);
                    Last_Crystal = Crystal_02;
                }
                else
                {
                    Reset_Crystals();
                }
                Crystal_Cooldown = true;
                Invoke("Reset_Crystal_Cooldown", 2f);

            }
            if (Crystal_03.OverlapPoint(Player.transform.position) && !Crystal_03.GetComponent<Crystal_Behviour>().IsActive)
            {

                if (Last_Crystal == Crystal_02)
                {
                    Crystal_03.GetComponent<Crystal_Behviour>().SetActive(true);
                    Last_Crystal = Crystal_03;
                }
                else
                {
                    Reset_Crystals();
                }
                Crystal_Cooldown = true;
                Invoke("Reset_Crystal_Cooldown", 2f);
            }
            if (Crystal_04.OverlapPoint(Player.transform.position) && !Crystal_04.GetComponent<Crystal_Behviour>().IsActive)
            {

                if (Last_Crystal == Crystal_03)
                {
                    Crystal_04.GetComponent<Crystal_Behviour>().SetActive(true);
                    Last_Crystal = Crystal_04;
                    GameManager.Gm.ReadActivated = true;
                    Door.SetActive(false);

}
                else
                {
                    Reset_Crystals();
                }
                Crystal_Cooldown = true;
                Invoke("Reset_Crystal_Cooldown", 2f);
            }
        }

    }

    private void Reset_Crystal_Cooldown()
    {
        Crystal_Cooldown = false;
    }

    private void Reset_Crystals()
    {
        Last_Crystal = null;
        Crystal_01.GetComponent<Crystal_Behviour>().SetActive(false);
        Crystal_02.GetComponent<Crystal_Behviour>().SetActive(false);
        Crystal_03.GetComponent<Crystal_Behviour>().SetActive(false);
        Crystal_04.GetComponent<Crystal_Behviour>().SetActive(false);
    }
}
