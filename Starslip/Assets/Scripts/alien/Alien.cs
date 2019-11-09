using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    Transform myTrans;
    Vector3 oldPosition;
    Vector3 targetPosition;

    public float speed = 1f;
    float speedCompensator = 0f;
    public int health = 100;
    public int damage = 10;
    public float flightPerimeter = 10f;

    float lerpTime;


    void Awake()
    {
        myTrans = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        oldPosition = myTrans.position;
        //set random target.x, square it, subtract
        // from U and square root to get target.y
        SetTargetPos();

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(myTrans.position, targetPosition) <= 1f)
        {
            SetTargetPos();
        }

        //transform.Translate(targetPosition * speed * Time.deltaTime);
        lerpTime += (speed+speedCompensator) / 10 * Time.deltaTime;
        myTrans.position = Vector3.Lerp(oldPosition,new Vector3(targetPosition.x,targetPosition.y,targetPosition.z), lerpTime); //TODO: set targetposition.z position to camera's z pos
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this);
        }
    }

    public void SetTargetPos()
    {
        oldPosition = myTrans.position;
        lerpTime = 0f;
        float targetX = Random.Range(-flightPerimeter, flightPerimeter);
        float targetY = Random.Range(-flightPerimeter, flightPerimeter);
        targetPosition = new Vector3(targetX, targetY, myTrans.position.z);
        if (Mathf.Abs((oldPosition.x+oldPosition.y)-(targetPosition.x+targetPosition.y))<=8f)
        {
            speedCompensator = 5;
        }
        else
        {
            speedCompensator = 0;
        }
    }
}
