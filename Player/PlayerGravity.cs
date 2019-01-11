using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    [HideInInspector]
    public Vector3 gravity;

    private PlayerController playerController;
    private Rigidbody objRb;
    private Quaternion objRotation;
    private float translationTime;
    private float timer;
    private bool rotating;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        objRb = GetComponent<Rigidbody>();
        gravity = new Vector3(0, -9.81f, 0);
    }

    private void Start()
    {
        translationTime = 50.0f;
        timer = translationTime;
        rotating = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !rotating && !playerController.dead && !playerController.cutscene && !playerController.preventMovement)
        {
            playerController.preventMovement = true;
            ChangeGravityDirection();
        }

        if (rotating)
        {
            RotatePlayer();
        }

        objRb.AddForce(gravity);
    }

    // Change gravity direction
    private void ChangeGravityDirection()
    {
        if (gravity.y == 9.81f)
        {
            gravity.y = -9.81f;
            objRotation = Quaternion.Euler(transform.eulerAngles - new Vector3(0f, 0f, 180f));
        }
        else if (gravity.y == -9.81f)
        {
            gravity.y = 9.81f;
            objRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 0f, 180f));
        }

        timer = 0.0f;
        rotating = true;
    }

    // Rotate player depending on gravity direction
    private void RotatePlayer()
    {
        if (timer <= translationTime)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, objRotation, timer / translationTime);
            timer += Time.deltaTime * 50f;
        }
        else
        {
            transform.rotation = objRotation;
            rotating = false;
            playerController.preventMovement = false;
        }
    }
}
