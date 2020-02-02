using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPatrol : MonoBehaviour
{
    public GameObject[] patrolPoints;

    public float moveSpeed = 10;
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
}
