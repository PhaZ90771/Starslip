using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien2 : MonoBehaviour
{
    Vector2 direction;
    public float Speed = 18;
    public float flightPerimeter = 5f;

    private void Awake()
    {
        Transform trans = transform;
        trans = GameObject.Find("SS Name Pending").transform;
    }

    private void Start()
    {
        direction = new Vector2(Random.Range(-flightPerimeter, flightPerimeter), Random.Range(-flightPerimeter, flightPerimeter));

    }

    private void Update()
    {
        Move();
        ClampPosition();
    }

    private void Move()
    {
        transform.Translate(direction * Speed * Time.deltaTime);
    }

    private void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

}
