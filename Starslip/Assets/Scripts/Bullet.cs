using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float LifeTime = 5f;
    public int Damage = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            var asteroid = collision.gameObject.GetComponent<Asteriod>();
            if (asteroid)
                asteroid.TakeDamage(Damage);
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
