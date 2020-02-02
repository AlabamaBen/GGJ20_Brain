using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video; // imports the UnityEngine.Video library


public class Autel_Behaviour : MonoBehaviour
{

    public SpriteRenderer crystal1;
    public SpriteRenderer crystal2;

    public Sprite crystalactive;
    public Sprite crystalunactive;

    public Image blackscreen;

    private bool startanim = false;
    private bool endanim = false;


    public float blackspeed = 0.1f;

    public VideoPlayer video; // VideoPLayer component 



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(endanim)
        {
            blackscreen.color = new Color(.0f, .0f, .0f, blackscreen.color.a - Time.deltaTime * blackspeed);
            
        }
        else
        {
            if (startanim)
            {
                blackscreen.color = new Color(.0f, .0f, .0f, blackscreen.color.a + Time.deltaTime * blackspeed);
                if(blackscreen.color.a >= 0.9f)
                {
                    video.gameObject.SetActive(true);
                    video.Play();
                }
                if (blackscreen.color.a >= 1f)
                {
                    endanim = true;
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if(GameManager.Gm.ColorActivated)
        {
            crystal1.sprite = crystalactive;
        }
        else
        {
            crystal1.sprite = crystalunactive;
        }
        if (GameManager.Gm.ReadActivated)
        {
            crystal2.sprite = crystalactive;
        }
        else
        {
            crystal2.sprite = crystalunactive;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(GameManager.Gm.ReadActivated && GameManager.Gm.ColorActivated)
            {
                startanim = true;
            }
        }
    }
}
