using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
