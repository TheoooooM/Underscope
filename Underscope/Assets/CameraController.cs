using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance; 
    
    public float BorderAdd = .1f;
    public Border[] borders;

    private void Awake()
    {
        Instance = this;
    }

    public void Stray(float ratio = 1)
    {
        transform.position += Vector3.up*BorderAdd*ratio;
        foreach (var border in borders)
        {
         border.Replace();   
        }
    }
    
}
