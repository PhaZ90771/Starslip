﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public GameObject[] Asteroids;
    public GameObject Alien;
    public Transform playerTrans;

    [HideInInspector]
    public bool alienDead = true;

    float timeStamp;
    int quadrant;

    //[HideInInspector]
    public List<int> spawnQuadrants;
    [HideInInspector]
    public int spawnCount;

    

    // Start is called before the first frame update
    void Start()
    {
        alienDead = true;
        timeStamp = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStamp+2f <= Time.time)
        {
            SpawnAsteroid();
            spawnCount++;
            timeStamp = Time.time;
            if (Random.Range(0, 4) > 2 && alienDead && (spawnCount > 4)) 
            {
                SpawnAlien();
            }
        }

    }

    void SpawnAsteroid()
    {
        quadrant = Random.Range(1, 5);
        Vector3 quadVector = Vector3.zero;
        switch (quadrant)
        {
            case 1:
                quadVector = new Vector3(Random.Range(-10, 0), Random.Range(0, 10));
                spawnQuadrants.Add(1);
                break;
            case 2:
                quadVector = new Vector3(Random.Range(0, 10), Random.Range(0, 10));
                spawnQuadrants.Add(2);
                break;
            case 3:
                quadVector = new Vector3(Random.Range(-10, 0), Random.Range(-10, 0));
                spawnQuadrants.Add(3);
                break;
            case 4:
                quadVector = new Vector3(Random.Range(0, 10), Random.Range(-10, 0));
                spawnQuadrants.Add(4);
                break;
            default:
                Debug.LogError("No quadrant");
                break;
        }
        Instantiate(Asteroids[Random.Range(0, Asteroids.Length)], gameObject.transform.position + quadVector, Quaternion.identity);

    }

    void SpawnAlien()
    {
        Alien.SetActive(true);
        alienDead = false;
    }

    public void AlienDead()
    {
        alienDead = true;
        spawnCount = 0;
        spawnQuadrants.RemoveRange(0, spawnQuadrants.Count);

    }
}
