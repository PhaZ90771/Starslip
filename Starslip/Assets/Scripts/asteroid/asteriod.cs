using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteriod : MonoBehaviour
{
    Transform myTrans;
    Vector3 rotation;

    public float rotationOffset = 50f;

    private void Awake()
    {
        myTrans = transform; 
    }

    // Start is called before the first frame update
    void Start()
    {
        //Random rotation (speed and dir)
        rotation.x = Random.Range(-rotationOffset, rotationOffset);
        rotation.y = Random.Range(-rotationOffset, rotationOffset);
        rotation.z = Random.Range(-rotationOffset, rotationOffset);

        Debug.Log(rotation);

        // set random size

        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate
        myTrans.Rotate(rotation * Time.deltaTime);

    }
}
