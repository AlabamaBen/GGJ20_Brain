using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synaps : MonoBehaviour
{
    private Animator animator;
    private bool isActivate = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isActivate)
        {
            animator.SetTrigger("Activate");
            isActivate = true;
            AudioManager.instance.Play("synaps");
        }
    }

}
