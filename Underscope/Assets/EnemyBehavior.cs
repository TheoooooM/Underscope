using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IDamagable
{
    public float speed = .2f;
    public int life = 5;

    private void Update()
    {
        transform.LookAt(new Vector3(PlayerController.Instance.transform.position.x, 0, PlayerController.Instance.transform.position.y));
    }

    void FixedUpdate()
    {
        Vector3 dir = (PlayerController.Instance.transform.position - transform.position);
        dir.y = 0;
        transform.position += dir.normalized * speed;
    }

    public void TakeDamage(int amount)
    {
        life -= amount;
        if (life<= 0)
        {
            Death();
        }
    }

    void Death()
    {
        gameObject.SetActive(false);
        CameraController.Instance.Stray();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage();
        }
    }
}
