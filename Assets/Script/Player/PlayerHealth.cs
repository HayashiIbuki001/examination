using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    /// <summary> –³“GŠÔ </summary>
    [SerializeField] private float invincibleTime = 1.0f;
    /// <summary> –³“GŠÔ‚©‚Ç‚¤‚© </summary>
    private bool isInvincible = false;

    /// <summary> ‘±ƒ_ƒ[ƒW‚Ì•b”ŠÔŠu </summary>
    [SerializeField] private float damageInterval = 1.0f;
    private float damageTimer = 0f;

    [SerializeField] private Slider hpBar;

    void Start()
    {
        currentHealth = maxHealth; // ‘Ì—Í‚ğ“¯Šú

        // HPƒo[‰Šúİ’è
        hpBar.maxValue = maxHealth;
        hpBar.value = currentHealth;
    }

    void TakeDamage(int damage)
    {
        if (isInvincible || currentHealth <= 0) return;

        currentHealth -= damage; // HP‚ğŒ¸‚ç‚·
        currentHealth = Mathf.Max(currentHealth, 0);
        Debug.Log("HP : " + currentHealth);
        
        hpBar.value = currentHealth;

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

    void DamageZone(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        hpBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            GameManager.Instance.isGameOver = true;
        }
    }

    

    /// <summary> –³“GŠÔ‚Ìˆ— </summary>
    private IEnumerator InvincibilityTime()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<IDamageDealer>(out var dealer))
            {
                TakeDamage(dealer.Damage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DamageZone")) // ‘±ƒ_ƒƒ][ƒ“‚É“ü‚é
        {
            if (collision.gameObject.TryGetComponent<IDamageDealer>(out var dealer))
            {
                damageTimer += Time.deltaTime;
                if (damageTimer >= damageInterval)
                {
                    DamageZone(dealer.Damage);
                    damageTimer = 0f; // ‰Šú‰»
                }
            }
        }
        else
        {
            damageTimer = 0f;
        } // ‘±ƒ_ƒƒ][ƒ“‚©‚ço‚½
    }
}
