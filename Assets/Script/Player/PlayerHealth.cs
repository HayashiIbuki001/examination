using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int health = 3;

    void Update()
    {
        
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //‚±‚±‚Éƒ_ƒˆ—
        }
    }
}
