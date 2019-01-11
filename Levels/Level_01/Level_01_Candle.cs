using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_01_Candle : MonoBehaviour
{
    public int candleNumber;
    public GameObject particles;
    public Level_01_Controller controller;

    private void Start()
    {
        candleNumber++;
    }

    private void Update()
    {
        if(controller.currentCandle == candleNumber)
        {
            BlowOut();
        }
    }

    private void BlowOut()
    {
        particles.SetActive(false);
    }
}
