using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHaracter_Debug : MonoBehaviour
{

    public float Move_Speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.Translate(input * Time.deltaTime * Move_Speed);
    }
}
