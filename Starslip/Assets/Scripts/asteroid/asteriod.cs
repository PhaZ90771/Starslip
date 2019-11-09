using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : MonoBehaviour
{
    Transform myTrans;
    Vector3 rotation;
    Vector3 size = Vector3.one;
    int health;
    int randAsteroidClass;

    public float rotationOffset = 75f;
    public int minClass = 1;
    public int maxClass = 5;
    public int damageDealt;
    


    void Awake()
    {
        myTrans = transform; 
    }

    // Start is called before the first frame update
    void Start()
    {
        //Random rotation
        rotation.x = Random.Range(-rotationOffset, rotationOffset);
        rotation.y = Random.Range(-rotationOffset, rotationOffset);
        rotation.z = Random.Range(-rotationOffset, rotationOffset);

        // set random size and health and damage
        randAsteroidClass = Random.Range(minClass, maxClass);
        size.Scale(Vector3.one * randAsteroidClass);
        myTrans.localScale = size;

        health = 100 * randAsteroidClass;

        damageDealt = 10 * randAsteroidClass;

    }

    // Update is called once per frame
    void Update()
    {
        //rotate
        myTrans.Rotate(rotation * Time.deltaTime);

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this);
        }
    }



}
