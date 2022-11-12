using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public float spawnCoolDown = 1.5f;
    private float timer;

    public GameObject enemyPrefab;
   

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            timer = spawnCoolDown;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float x = 0;
        float y = 0;

        int rdm = Random.Range(0, 4);

        switch (rdm)
        {
            case 0:
                x = Random.Range(1.1f, 1.3f);
                y = Random.Range(-.3f, 1.2f);
                break;
            case 1:
                x = Random.Range(-.3f, -.1f);
                y = Random.Range(-.3f, 1.2f);
                break;
            case 2:
                y = Random.Range(1.1f, 1.3f);
                x = Random.Range(-.3f, 1.2f);
                break;
            case 3:
                y = Random.Range(-.3f, -.1f);
                x = Random.Range(-.3f, 1.2f);
                break;
        }

        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(x,y,10));
        pos.y = .5f;
        Instantiate(enemyPrefab, pos, Quaternion.identity, transform);
    }
}
