using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private PlayerInput input;
    
    public float rotateSpeed = 1;
    public float moveSpeed = 1;
    private bool moving;
    private bool canMove = true;
    public float life = 3;

    private Rigidbody rb;

    private void Awake()
    {
        SetInput();
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void SetInput()
    {
        input = new PlayerInput();
        input.Enable();
        input.Movement.Move.started += context => moving = true;
        input.Movement.Move.canceled += context => moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(new Vector3(0,-rotateSpeed* Time.deltaTime,0));
        if (Input.GetKey(KeyCode.RightArrow)) transform.Rotate(new Vector3(0,rotateSpeed* Time.deltaTime,0));

        if (moving && canMove) Move();
        PointView();
    }

    void PointView()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        mousePos.y = transform.position.y;
        transform.LookAt(mousePos);
    }

    private void Move()
    {
        Vector2 dir = input.Movement.Move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(dir.x, 0, dir.y) * moveSpeed * Time.deltaTime;
        rb.AddForce(movement.normalized*moveSpeed, ForceMode.Force);
    }

    public void TakeDamage()
    {
        life -= 1;
        if (life == 0)
        {
            Time.timeScale = 0;
        }
    }
}
