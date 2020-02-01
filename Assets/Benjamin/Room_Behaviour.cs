using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Behaviour : MonoBehaviour
{

    public GameObject Camera_Position;

    public bool Camera_Follow = false;

    public BoxCollider2D room_collider;

    private Camera_Manager camera_manager;

    public GameObject Respawn_Point;

    // Start is called before the first frame update
    void Start()
    {
        room_collider = GetComponent<BoxCollider2D>();

        camera_manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(camera_manager.Current_Room != gameObject)
            {
                camera_manager.Change_Room(gameObject);
            }
        }
    }


}
