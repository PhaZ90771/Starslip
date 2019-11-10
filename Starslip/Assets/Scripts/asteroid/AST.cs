using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AST : MonoBehaviour
{
    Transform myTrans;
    Vector3 rotation;
    Vector3 size = Vector3.one;
    float randScale;

    public float rotationOffset = 75f;
    public float minScale = 20f;
    public float maxScale = 80f;
    public int health = 100;
    public int damageDealt = 10;
    


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
        randScale = Random.Range(minScale, maxScale);
        size.Scale(Vector3.one * randScale);
        myTrans.localScale = size;

        Destroy(gameObject, 24);
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate
        myTrans.Rotate(rotation * Time.deltaTime);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
 
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
