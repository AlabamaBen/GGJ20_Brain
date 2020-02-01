using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class Room02_Behaviour : MonoBehaviour
{
    public Collider2D Door_Locker;
    public Collider2D Synapse_Trigger;

    public GameObject Door;

    private GameObject Player; 

    private bool Doorlocked = false;
    private bool SynapseActivated = false;

    public PostProcessVolume pp;

    private ColorGrading _colorgrading;

    public float Color_Lerp_Speed = 3f; 

    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        pp.profile.TryGetSettings(out _colorgrading);

        _colorgrading.saturation.value = -100;

    }

    // Update is called once per frame
    void Update()
    {
        if(SynapseActivated)
        {
            _colorgrading.saturation.value = Mathf.Lerp(_colorgrading.saturation.value, 0, Color_Lerp_Speed * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if (!Doorlocked && Door_Locker.OverlapPoint(Player.transform.position))
        {
            Door.SetActive(true);
            Doorlocked = true; 
        }
        if(Doorlocked && !SynapseActivated && Synapse_Trigger.OverlapPoint(Player.transform.position))
        {
            Door.SetActive(false);
            SynapseActivated = true;
            GameManager.Gm.ColorActivated = true; 
        }
    }
}
