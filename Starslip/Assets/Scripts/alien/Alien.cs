using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    Transform mainCam;
    Transform myTrans;
    Vector3 oldPosition;
    Vector3 targetPosition;

    public float speed = 1f;
    public int health = 100;
    public int damage = 10;
    public float flightPerimeter = 10f;

    float lerpTime;

    float timeStamp;







    void Awake()
    {
        myTrans = transform;
        mainCam = Camera.main.transform;

    }

    // Start is called before the first frame update
    void Start()
    {
        timeStamp = Time.time;
        SetStartPos();
        oldPosition = myTrans.position;
        SetTargetPos();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (timeStamp+4f<=Time.time)
        {
            SetTargetPos();
            timeStamp = Time.time;
        }*/

        //transform.Translate(targetPosition * speed * Time.deltaTime);
        lerpTime += speed / 10 * Time.deltaTime;
        //transform.localRotation = mainCam.rotation;
        //myTrans.position = Vector3.Lerp(oldPosition+mainCam.position,targetPosition+mainCam.position, lerpTime);
        SetStartPos();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this);
        }
    }

    void SetTargetPos()
    {
        oldPosition = myTrans.position;
        lerpTime = 0f;
        float targetXYZ = Random.Range(-flightPerimeter, flightPerimeter);
        targetPosition = new Vector3(targetXYZ, targetXYZ, targetXYZ);
        
    }

    void SetStartPos()
    {
        myTrans.position = mainCam.position+(Vector3.forward*10);//new Vector3(mainCam.position.x, mainCam.position.y, mainCam.position.z + 20);
        myTrans.rotation = mainCam.rotation;
    }
}
