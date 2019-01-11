using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce;

    private PlayerController playerController;
    private PlayerGravity gravityScript;
    private Rigidbody rb;
    private Animator anim;
    private float distToGround;
    private Vector3 ray1;
    private Vector3 ray2;
    private Vector3 ray3;
    private Vector3 ray4;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        gravityScript = GetComponent<PlayerGravity>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        distToGround = 0.2f;
        ray1 = new Vector3(0.15f, -0.7f, 0);
        ray2 = new Vector3(-0.15f, -0.7f, 0);
        ray3 = new Vector3(0, -0.7f, 0.15f);
        ray4 = new Vector3(0, -0.7f, -0.15f);
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position + ray1, Vector3.down * distToGround, Color.green);
        //Debug.DrawRay(transform.position + ray2, Vector3.down * distToGround, Color.green);
        //Debug.DrawRay(transform.position + ray3, Vector3.down * distToGround, Color.green);
        //Debug.DrawRay(transform.position + ray4, Vector3.down * distToGround, Color.green);
        if (IsGrounded() && Input.GetButtonDown("Jump") && !playerController.dead && !playerController.cutscene && !playerController.paused && !playerController.preventMovement)
        {
            Debug.Log(gravityScript.gravity);
            if (gravityScript.gravity.y == -9.81f)
            {
                Debug.Log("Normal gravity jump");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else if (gravityScript.gravity.y == 9.81f)
            {
                Debug.Log("Inverted gravity jump");
                rb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
            }
        }

        anim.SetBool("IsFalling", !IsGrounded());
    }

    // Check if object is Grounded
    private bool IsGrounded()
    {
        if (gravityScript.gravity.y == -9.81f)
        {
            return Physics.Raycast(transform.position + ray1, Vector3.down, distToGround) || Physics.Raycast(transform.position + ray2, Vector3.down, distToGround) || Physics.Raycast(transform.position + ray3, Vector3.down, distToGround) || Physics.Raycast(transform.position + ray4, Vector3.down, distToGround);
        }
        else
        {
            return Physics.Raycast(transform.position - ray1, Vector3.up, distToGround) || Physics.Raycast(transform.position - ray2, Vector3.up, distToGround) || Physics.Raycast(transform.position - ray3, Vector3.up, distToGround) || Physics.Raycast(transform.position - ray4, Vector3.up, distToGround);
        }
    }
}
