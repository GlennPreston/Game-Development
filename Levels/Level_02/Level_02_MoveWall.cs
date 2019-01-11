using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_02_MoveWall : MonoBehaviour
{
    public Collider playerCollider;
    public Rigidbody wallRb;
    public Vector3 moveDist;
    public float speed;

    private bool activated;
    private bool moving;
    private Vector3 endPosition;
    private Vector3 dir;
    private float vector3Dist;

    private void Start()
    {
        activated = false;
        moving = false;
        endPosition = wallRb.position + moveDist;
        dir = (wallRb.position - endPosition).normalized;
        vector3Dist = 0.01f;
    }

    private void FixedUpdate()
    {
        if(moving)
        {
            MoveWall();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider == playerCollider && !activated)
        {
            activated = true;
            moving = true;
        }
    }

    private void MoveWall()
    {
        if(vector3Dist > Vector3.Distance(wallRb.position, endPosition))
        {
            moving = false;
        }
        else
        {
            wallRb.MovePosition(wallRb.position - dir * speed * Time.deltaTime);
        }
    }
}
