using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Behviour : MonoBehaviour
{

    public Sprite sprite_active;
    public Sprite sprite_unactive;

    private SpriteRenderer sprite_render; 

    private bool IsActive; 


    // Start is called before the first frame update
    void Start()
    {
        IsActive = false;

        sprite_render = GetComponent<SpriteRenderer>();

        SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetActive(bool active)
    {
        IsActive = active;
        if (active)
        {
            sprite_render.sprite = sprite_active;
        }
        else
        {
            sprite_render.sprite = sprite_unactive;
        }
    }


}
