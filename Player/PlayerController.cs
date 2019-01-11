using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool dead;
    public bool cutscene;
    public bool paused;
    public bool preventMovement;

    private void Awake()
    {
        dead = false;
        cutscene = false;
        paused = false;
        preventMovement = false;
    }
}
