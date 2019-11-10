using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public GameObject[] Asteroids;

    float timeStamp;


    // Start is called before the first frame update
    void Start()
    {
        timeStamp = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        if (timeStamp+2f <= Time.time)
        {
            Spawn();
            timeStamp = Time.time;
        }

    }

    void Spawn()
    {
        Instantiate(Asteroids[Random.Range(0, Asteroids.Length)],gameObject.transform.position+Vector3.one*Random.Range(-10,10),Quaternion.identity);

    }
}
