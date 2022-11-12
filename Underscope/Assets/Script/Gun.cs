using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Gun Instance;
    
    [Header("Attributes")]
    public float firerate = 1f;
    private float timer;
    public int damage = 10;
    public int bulletNumber = 1;
    public float shotLatency = .1f;
    public bool randomLatency;

    public int maxAmmo = 20;

    //Patern
    
    [Header("Reload Info")]
    public float standardReload = 3.0f;
    public float activeReload = 2.25f;
    public float perfectReload = 1.8f;
    public float failedReload = 4.1f;
    public float bonusTime = 1f;

    [Header("Current")]
    public int currentAmmo = 0;
    public int currentClip = 0;

    public State state = State.READY;
    public enum State { READY, FIRING, RELOADING,};

    public Reload reload;
    
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

    private float _currentFireRate;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timer = firerate;
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            if (currentAmmo > 0 && state == State.READY)
            {
                Shoot();
                timer = firerate;
            }
            else if (currentAmmo <= 0 && state == State.READY)
            {
                reload.BeginReload();
                state = State.RELOADING;
            }
            else
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && state == State.READY)
        {
            reload.BeginReload();
            state = State.RELOADING;
        }
    }

    public void PerfectReload()
    {
        currentAmmo = maxAmmo;
        state = State.READY;

        StartCoroutine(PerfectBonus());
    }
    
    public void ActiveReload()
    {
        currentAmmo = maxAmmo;
        state = State.READY;
    }

    void Shoot()
    {
        state = State.FIRING;
        currentAmmo--;
        Debug.Log(currentAmmo);
        
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

        state = State.READY;
    }

    private IEnumerator PerfectBonus()
    {
        _currentFireRate = firerate;
        float newFireRate = firerate * 0.5f;
        firerate = newFireRate;
        
        yield return new WaitForSeconds(bonusTime);
        firerate = _currentFireRate;
    }
}
