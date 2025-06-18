using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    /// <summary> –³“GŠÔ </summary>
    [SerializeField] private float invincibleTime = 1.0f;
    /// <summary> –³“GŠÔ‚©‚Ç‚¤‚© </summary>
    public bool isInvincible = false;

    /// <summary> ‘±ƒ_ƒ[ƒW‚Ì•b”ŠÔŠu </summary>
    [SerializeField] private float damageInterval = 1.0f;
    private float damageTimer = 0f;
    public AudioClip damageSE;
    AudioSource audiosource;

    [SerializeField] private Slider hpBar;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        hpBar = GameObject.Find("HPBar").GetComponent<Slider>();

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

        hpBar.DOValue(currentHealth, 0.5f);

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

        hpBar.DOValue(currentHealth, 0.5f);

        if (currentHealth <= 0)
        {
            GameManager.Instance.isGameOver = true;
        }
    }

    

    /// <summary> –³“GŠÔ‚Ìˆ— </summary>
    private IEnumerator InvincibilityTime()
    {
        isInvincible = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.5f); // Ô,”¼“§–¾

        audiosource.PlayOneShot(damageSE);

        yield return new WaitForSeconds(invincibleTime);

        GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f); // –ß‚·
        GetComponent<Collider2D>().enabled = true;
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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hpBar = GameObject.Find("HPBar").GetComponent<Slider>();

        if (hpBar != null)
        {
            hpBar.maxValue = maxHealth;
            hpBar.value = currentHealth; // ‘Ì—Í‚ğ“¯Šú
        }
    }

}
