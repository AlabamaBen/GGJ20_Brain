using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock_Behvaiour : MonoBehaviour
{
    public GameObject Pick01;
    public GameObject Pick02;

    private List<GameObject> Platefroms; 

    Plateform_Color current_color; 

    public float Frequency = 1f; 
    // Start is called before the first frame update
    void Start()
    {

        current_color = 0;
        InvokeRepeating("Tick", Frequency, Frequency);
        Platefroms = new List<GameObject>();
        Clock_Plateform[] plateforms_bhvs = FindObjectsOfType<Clock_Plateform>();
        foreach (Clock_Plateform Clock_Plateform in plateforms_bhvs)
        {
            Platefroms.Add(Clock_Plateform.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    void Tick()
    {

        current_color = (Plateform_Color)((int)(current_color + 1) % 3) ;


        Pick01.transform.rotation =  Quaternion.Euler(0f, 0f, (360 / 3) * -((int)current_color));

        Pick02.transform.rotation = Quaternion.Euler(0f, 0f, (360 / 3) * -((int)current_color));



        foreach (GameObject plateform in Platefroms)
        {

            plateform.SetActive(!(plateform.GetComponent<Clock_Plateform>().color == current_color));
        }

    }
}
