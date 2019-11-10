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
    float centerX = 10f, centerY = 8f;

    private void Start()
    {
        Debug.Log("Alien Started");
        AsteroidSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<AsteroidSpawn>();
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
        transform.localPosition = Vector3.Lerp(transform.localPosition, quadOffset, 0.05f);
    }

    public void ChangeOffset()
    {
        int quad = AsteroidSpawner.spawnQuadrants[1];
        switch (quad)
        {
            case 1:
                quadOffset = new Vector3(centerX, centerY)* Random.Range(0.8f,1.2f);
                break;
            case 2:
                quadOffset = new Vector3(-centerX, centerY) * Random.Range(0.8f, 1.2f);
                break;
            case 3:
                quadOffset = new Vector3(centerX, -centerY) * Random.Range(0.8f, 1.2f);
                break;
            case 4:
                quadOffset = new Vector3(-centerX, -centerY) * Random.Range(0.8f, 1.2f);
                break;
            default:
                Debug.LogError("No Quadrant Alien script");
                break;
        }

        AsteroidSpawner.spawnQuadrants.RemoveAt(0);

    }
    public void TakeDamage(int damage)
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        health -= damage;
    }
}