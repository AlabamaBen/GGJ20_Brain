using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    public static Camera_Manager instance = null;
    public GameObject Default_Room;
    private GameObject target;
    public GameObject Current_Room;
    public GameObject Character;

    private bool following;

    public float Camera_Offset = 4f;
    public enum ShakeCamType { LandShake,DeathShake, UnlockShake};
    //private Animator cameraAnimator;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        target = Default_Room.GetComponent<Room_Behaviour>().Camera_Position;

        following = false;
        //cameraAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         

        if(following)
        {

            Room_Behaviour room = Current_Room.GetComponent<Room_Behaviour>();
            float max_follow = ((room.room_collider.bounds.size.x) - (Screen.width / 100)) / Camera_Offset;

            Vector3 newPosition = new Vector3(Mathf.Clamp(target.transform.position.x,
                room.Camera_Position.transform.position.x - max_follow,
                room.Camera_Position.transform.position.x + max_follow), room.Camera_Position.transform.position.y, transform.position.z); ;


            Vector3 move_vector = Vector3.Slerp(transform.position, newPosition, 5f * Time.deltaTime);

            transform.position = move_vector;

        }
        else
        {

            Vector3 newPosition = target.transform.position;
            newPosition.z = transform.position.z;


            transform.position = Vector3.Slerp(transform.position, newPosition, 5f * Time.deltaTime);
        }




    }

    public void Change_Room(GameObject new_room)
    {
        Room_Behaviour room = new_room.GetComponent<Room_Behaviour>();

        Current_Room = new_room;

        if (room.Camera_Follow)
        {
            target = Character;
            following = true;
        }
        else
        {
            target = room.Camera_Position;
            following = false;
        }
    }
    /*
    public void ShakeCamera(ShakeCamType shakeCamType)
    {
        cameraAnimator.SetTrigger(shakeCamType.ToString());
    }
    */
}
