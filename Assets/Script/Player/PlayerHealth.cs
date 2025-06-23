using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    /// <summary> ���G���� </summary>
    [SerializeField] private float invincibleTime = 1.0f;
    /// <summary> ���G���Ԃ��ǂ��� </summary>
    public bool isInvincible = false;

    /// <summary> �����_���[�W�̕b���Ԋu </summary>
    [SerializeField] private float damageInterval = 1.0f;
    private float damageTimer = 0f;
    public AudioClip damageSE;
    AudioSource audiosource;

    [SerializeField] private Slider hpBar;
    [SerializeField] private CanvasGroup effectCanvasGroup;

    void Start()
    {
        effectCanvasGroup = GameObject.Find("DamageEffect").GetComponent<CanvasGroup>();
        effectCanvasGroup.alpha = 0; // �_���[�W�G�t�F�N�g�̉摜�𓧖���

        audiosource = GetComponent<AudioSource>();
        hpBar = GameObject.Find("HPBar").GetComponent<Slider>();

        currentHealth = maxHealth; // �̗͂𓯊�

        // HP�o�[�����ݒ�
        hpBar.maxValue = maxHealth;
        hpBar.value = currentHealth;
    }

    void TakeDamage(int damage)
    {
        if (isInvincible || currentHealth <= 0) return;

        currentHealth -= damage; // HP�����炷
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
            Debug.Log($"{invincibleTime}�b���G");
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
        effectCanvasGroup.alpha = 1; // �_���[�W�G�t�F�N�g�摜��\��
        yield return new WaitForSeconds(0.1f);

        while (effectCanvasGroup.alpha > 0)
        {
            effectCanvasGroup.alpha -= Time.deltaTime * 2; // �����Â�����
            yield return null; 
        } // 0�ɂȂ�����Ԃ�
    }


    /// <summary> ���G���Ԃ̏��� </summary>
    private IEnumerator InvincibilityTime()
    {
        isInvincible = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.5f); // ��,������

        audiosource.PlayOneShot(damageSE);

        yield return new WaitForSeconds(invincibleTime);

        GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f); // �߂�
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
        if (collision.gameObject.CompareTag("DamageZone")) // �����_���]�[���ɓ���
        {
            if (collision.gameObject.TryGetComponent<IDamageDealer>(out var dealer))
            {
                damageTimer += Time.deltaTime;
                GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.5f); // ��,������
                if (damageTimer >= damageInterval)
                {
                    DamageZone(dealer.Damage);
                    damageTimer = 0f; // ������
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
