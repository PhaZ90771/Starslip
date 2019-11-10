using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float LifeTime = 5f;
    public int Damage = 5;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Asteroid")
        {
            collision.gameObject.GetComponent<Asteriod>().TakeDamage(Damage);
            LifeTime = 0f;
        }
    }

    private void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
