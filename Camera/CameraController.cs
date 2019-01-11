using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public PlayerController playerController;
    public PlayerGravity gravityScript;
    public float clampMax;
    public float clampMin;
    public LayerMask wallLayer;

    private float currentX;
    private float currentY;
    private Vector3 camPos;
    private Vector3 lookAt;
    private Quaternion rotation;

    private void Start()
    {
        camPos = transform.position;
    }

    void Update()
    {
        if (!playerController.dead && !playerController.cutscene && !playerController.paused)
        {
            if (gravityScript.gravity.y == -9.81f)
            {
                currentX += Input.GetAxis("Mouse X");
                currentY += Input.GetAxis("Mouse Y");
            }
            else if (gravityScript.gravity.y == 9.81f)
            {
                currentX -= Input.GetAxis("Mouse X");
                currentY -= Input.GetAxis("Mouse Y");
            }
        }

        currentY = Mathf.Clamp(currentY, clampMin, clampMax);
    }

    void LateUpdate()
    {
        lookAt = player.position;

        if (gravityScript.gravity.y == -9.81f)
        {
            rotation = Quaternion.Euler(-currentY, currentX, 0);
        }
        else if (gravityScript.gravity.y == 9.81f)
        {
            rotation = Quaternion.Euler(-currentY, currentX, 0);
        }

        transform.position = lookAt + rotation * camPos;
        transform.LookAt(lookAt, player.up);

        CheckWall();
    }

    void CheckWall()
    {
        RaycastHit hit;

        if (Physics.Linecast(lookAt, transform.position, out hit, wallLayer))
        {
            Debug.Log("Hit");
            transform.position = lookAt + ((transform.position - lookAt).normalized * hit.distance);
        }
    }
}
