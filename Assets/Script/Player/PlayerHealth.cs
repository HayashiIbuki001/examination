using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    /// <summary> 無敵時間 </summary>
    [SerializeField] private float invincibleTime = 1.0f;
    /// <summary> 無敵時間かどうか </summary>
    public bool isInvincible = false;

    /// <summary> 持続ダメージの秒数間隔 </summary>
    [SerializeField] private float damageInterval = 1.0f;
    private float damageTimer = 0f;
    public AudioClip damageSE;
    AudioSource audiosource;

    [SerializeField] private Slider hpBar;
    [SerializeField] private CanvasGroup effectCanvasGroup;

    void Start()
    {
        effectCanvasGroup = GameObject.Find("DamageEffect").GetComponent<CanvasGroup>();
        effectCanvasGroup.alpha = 0; // ダメージエフェクトの画像を透明に

        audiosource = GetComponent<AudioSource>();
        hpBar = GameObject.Find("HPBar").GetComponent<Slider>();

        currentHealth = maxHealth; // 体力を同期

        // HPバー初期設定
        hpBar.maxValue = maxHealth;
        hpBar.value = currentHealth;
    }

    void TakeDamage(int damage)
    {
        if (isInvincible || currentHealth <= 0) return;

        currentHealth -= damage; // HPを減らす
        currentHealth = Mathf.Max(currentHealth, 0);
        Debug.Log("HP : " + currentHealth);

        StartCoroutine(DamageEffect());
        hpBar.DOValue(currentHealth, 0.5f);

        if (currentHealth <= 0)
        {
            GameManager.Instance.isGameOver = true;
        }
        else
        {
            Debug.Log($"{invincibleTime}秒無敵");
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

    IEnumerator DamageEffect()
    {
        effectCanvasGroup.alpha = 1; // ダメージエフェクト画像を表示
        yield return new WaitForSeconds(0.1f);

        while (effectCanvasGroup.alpha > 0)
        {
            effectCanvasGroup.alpha -= Time.deltaTime * 2; // 少しづつ透明に
            yield return null; 
        } // 0になったら返す
    }


    /// <summary> 無敵時間の処理 </summary>
    private IEnumerator InvincibilityTime()
    {
        isInvincible = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.5f); // 赤,半透明

        audiosource.PlayOneShot(damageSE);

        yield return new WaitForSeconds(invincibleTime);

        GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f); // 戻す
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
        if (collision.gameObject.CompareTag("DamageZone")) // 持続ダメゾーンに入る
        {
            if (collision.gameObject.TryGetComponent<IDamageDealer>(out var dealer))
            {
                damageTimer += Time.deltaTime;
                GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.5f); // 赤,半透明
                if (damageTimer >= damageInterval)
                {
                    DamageZone(dealer.Damage);
                    damageTimer = 0f; // 初期化
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DamageZone"))
        {
            GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
            damageTimer = 0f;
        }
    }
}
