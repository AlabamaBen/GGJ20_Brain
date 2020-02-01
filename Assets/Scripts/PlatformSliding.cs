using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSliding : MonoBehaviour
{
    public GameObject[] patrolPoints;

    public float moveSpeed;
    private int currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = patrolPoints[0].transform.position;
        currentPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == patrolPoints[currentPoint].transform.position)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].transform.position, moveSpeed * Time.deltaTime);
    }

    public void SetMove(float f)
    {
        moveSpeed = f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(this.transform);
        }

    }

}
