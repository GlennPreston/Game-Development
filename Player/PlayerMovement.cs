using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    private PlayerController playerController;
    private PlayerGravity gravityScript;
    private Vector3 movement;
    private Animator anim;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        gravityScript = GetComponent<PlayerGravity>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!playerController.dead && !playerController.cutscene && !playerController.paused && !playerController.preventMovement)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (h != 0f || v != 0f)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Running(h, v);
                }
                else
                {
                    Walking(h, v);
                }
            }
            else
            {
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRunning", false);
            }
        }
    }

    void Walking(float h, float v)
    {
        movement.Set(h, 0f, v);
        Vector3 dir = movement.normalized;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0.0f;
        transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);

        if (gravityScript.gravity.y == -9.81f)
        {
            transform.forward = Vector3.Slerp(transform.forward, dir, turnSpeed);
        }
        else if (gravityScript.gravity.y == 9.81f)
        {
            transform.forward = Vector3.Slerp(transform.forward, dir, turnSpeed);
            transform.rotation = Quaternion.LookRotation(transform.forward, -transform.up);
        }

        anim.SetBool("IsRunning", false);
        anim.SetBool("IsWalking", true);
    }

    void Running(float h, float v)
    {
        movement.Set(h, 0f, v);
        Vector3 dir = movement.normalized;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0.0f;
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

        if (gravityScript.gravity.y == -9.81f)
        {
            transform.forward = Vector3.Slerp(transform.forward, dir, turnSpeed);
        }
        else if (gravityScript.gravity.y == 9.81f)
        {
            transform.forward = Vector3.Slerp(transform.forward, dir, turnSpeed);
            transform.rotation = Quaternion.LookRotation(transform.forward, -transform.up);
        }

        anim.SetBool("IsWalking", false);
        anim.SetBool("IsRunning", true);
    }
}
