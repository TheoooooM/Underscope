using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance; 
    
    public float BorderAdd = .1f;
    public float moveSpeed = 0.01f;
    public Border[] borders;

    private float heightPos;

    private void Awake()
    {
        Instance = this;
        heightPos = transform.position.y;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < heightPos)
        {
            transform.position += Vector3.up*moveSpeed;
            foreach (var border in borders)
            {
                border.Replace();   
            }
        }
    }

    public void Stray(float ratio = 1)
    {
        heightPos += BorderAdd*ratio;
        foreach (var border in borders)
        {
         border.Replace();   
        }
    }
    
}
