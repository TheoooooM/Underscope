using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public bool debug;
    
    void FixedUpdate()
    {
        transform.position += transform.forward*bulletSpeed;
    }

    private void Update()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if(debug)Debug.Log(viewportPos);
        if(viewportPos.x<-.1f || viewportPos.x>1.1 || viewportPos.y<-.1f || viewportPos.y>1.1) gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Gun.Instance.bulletQueue.Enqueue(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable hit = other.GetComponent<IDamagable>();
        if (hit != null)
        {
            hit.TakeDamage(Gun.Instance.damage);
            gameObject.SetActive(false);
        }
    }
}
