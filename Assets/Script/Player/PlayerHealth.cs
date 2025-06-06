using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    /// <summary> –³“GŠÔ </summary>
    [SerializeField] private float invincibleTime = 1.0f;
    /// <summary> –³“GŠÔ‚©‚Ç‚¤‚© </summary>
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth; // ‘Ì—Í‚ğ“¯Šú
    }

    void TakeDamage(int damage)
    {
        if (isInvincible || currentHealth <= 0) return;

        currentHealth -= damage; // HP‚ğŒ¸‚ç‚·
        Debug.Log("HP : " + currentHealth);

        if (currentHealth <= 0)
        {
            GameManager.Instance.isGameOver = true;
        }
        else
        {
            Debug.Log($"{invincibleTime}•b–³“G");
            StartCoroutine(InvincibilityTime());
        }
    }

    /// <summary> –³“GŠÔ‚Ìˆ— </summary>
    private IEnumerator InvincibilityTime()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageDealer>(out var dealer))
        {
            TakeDamage(dealer.Damage);
        }
    }
}
