using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_02_Controller : MonoBehaviour
{
    [HideInInspector]
    public bool completed;
    [HideInInspector]
    public int currentCandle;

    public Collider playerCollider;
    public Rigidbody leftDoor;
    public Rigidbody rightDoor;
    public float speed;
    public Vector3 moveDist;

    private bool moving;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 dir;
    private float vector3Dist;

    private void Start()
    {
        currentCandle = 1;
        completed = false;

        moving = false;
        startPosition = leftDoor.position;
        endPosition = startPosition + moveDist;
        dir = (startPosition - endPosition).normalized;
        vector3Dist = 0.01f;
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            if (vector3Dist > Vector3.Distance(leftDoor.position, endPosition))
            {
                moving = false;
            }
            else
            {
                MoveDoors();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider == playerCollider && !completed)
        {
            completed = true;
            moving = true;
        }
    }

    private void MoveDoors()
    {
        leftDoor.MovePosition(leftDoor.position - dir * speed * Time.deltaTime);
        rightDoor.MovePosition(rightDoor.position + dir * speed * Time.deltaTime);
    }
}
