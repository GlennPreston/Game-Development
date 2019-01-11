using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_01_Button : MonoBehaviour
{
    public Collider playerCollider;
    public GameObject interactOverlay;
    public Level_01_Controller controller;
    public string colour;

    private bool canInteract;

    private void Start()
    {
        canInteract = false;
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown("e"))
        {
            controller.buttonPressed(colour);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider == playerCollider)
        {
            interactOverlay.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider == playerCollider)
        {
            interactOverlay.SetActive(false);
            canInteract = false;
        }
    }
}
