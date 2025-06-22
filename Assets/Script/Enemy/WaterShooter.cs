using UnityEngine;

public class WaterShooter : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject bulletObj;
    public Transform firePoint;

    private float timer;
    [SerializeField] float fireInterval = 1f;
    public float speed = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireInterval )
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletObj, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = firePoint.right * speed;
    }
}
