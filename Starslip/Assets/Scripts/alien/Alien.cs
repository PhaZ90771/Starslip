using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public int health = 100;
    public int damage = 10;

    public Transform parent;
    
    Vector3 quadOffset;
    AsteroidSpawn AsteroidSpawner;
    float timeStamp;

    private void Start()
    {
        AsteroidSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<AsteroidSpawn>();
        AsteroidSpawner.spawnQuadrants.RemoveRange(0, 5);
        ChangeOffset();
        timeStamp = Time.time;
    }

    private void Update()
    {
        //transform.position = parent.position+quadOffset;
        if (timeStamp+2f <= Time.time)
        {
            timeStamp = Time.time;
            ChangeOffset();
        }
        Move();
    }

    public void Move()
    {
        Vector3.Lerp(transform.position, parent.position + quadOffset, Time.deltaTime*1);

    }

    public void ChangeOffset()
    {
        int quad = AsteroidSpawner.spawnQuadrants[0];
        switch (quad)
        {
            case 1:
                quadOffset = new Vector3(5, 5);
                break;
            case 2:
                quadOffset = new Vector3(-5, 5);
                break;
            case 3:
                quadOffset = new Vector3(5, -5);
                break;
            case 4:
                quadOffset = new Vector3(-5, -5);
                break;
            default:
                Debug.LogError("No Quadrant Alien script");
                break;
        }

        AsteroidSpawner.spawnQuadrants.RemoveAt(0);

    }
}