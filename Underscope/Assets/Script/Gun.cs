using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Gun Instance;
    
    public float firerate = 1f;
    private float timer;
    public int damage = 10;
    public int bulletNumber = 1;
    public float shotLatency = .1f;
    public bool randomLatency;
    //Patern
    
    public bool coneShoot;
    public bool patern;
    public float paternAngle  = 45f;
    
    public Vector2 bulletSize = new Vector2(1,1);
    public float moveShot = 10f;
    
    public float bulletSpeed = .1f;
    public AnimationCurve speedCurve = AnimationCurve.Constant(0,1,1);
    [Space] 
    public GameObject bulletPrefab;
    public Transform BulletPool;
    public Queue<GameObject> bulletQueue = new Queue<GameObject>();


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timer = firerate;
    }

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            Shoot();
            timer = firerate;
        }
    }

    void Shoot()
    {
        InstantiateBullet();
    }

    void InstantiateBullet()
    {
        Vector3 shootPos = transform.position + (transform.position - transform.parent.position)* (transform.localScale.z / 2);
        if (bulletQueue.Count == 0)
        {
            Instantiate(bulletPrefab, shootPos, transform.parent.rotation, BulletPool);
        }
        else
        {
            GameObject go = bulletQueue.Dequeue();
            go.transform.position = shootPos;
            go.transform.rotation = transform.parent.rotation;
            go.SetActive(true);
        }
    }
}
