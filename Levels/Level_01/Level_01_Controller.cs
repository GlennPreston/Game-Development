using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_01_Controller : MonoBehaviour
{
    [HideInInspector]
    public bool completed;
    [HideInInspector]
    public int currentCandle;

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

    private void Update()
    {
        if(currentCandle == 9 && !completed)
        {
            completed = true;
            moving = true;
        }
    }

    private void FixedUpdate()
    {
        if(moving)
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

    public void buttonPressed(string colour)
    {
        if(colour == "red" && (currentCandle == 1 || currentCandle == 4 || currentCandle == 8))
        {
            currentCandle++;
        }
        else if(colour == "green" && (currentCandle == 3 || currentCandle == 5 || currentCandle == 7))
        {
            currentCandle++;
        }
        else if(colour == "blue" && (currentCandle == 2 || currentCandle == 6))
        {
            currentCandle++;
        }
    }

    private void MoveDoors()
    {
        leftDoor.MovePosition(leftDoor.position - dir * speed * Time.deltaTime);
        rightDoor.MovePosition(rightDoor.position + dir * speed * Time.deltaTime);
    }
}
