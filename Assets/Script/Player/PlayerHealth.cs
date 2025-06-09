using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    /// <summary> ���G���� </summary>
    [SerializeField] private float invincibleTime = 1.0f;
    /// <summary> ���G���Ԃ��ǂ��� </summary>
    private bool isInvincible = false;

    [SerializeField] private Slider hpBar;

    void Start()
    {
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
        
        hpBar.value = currentHealth;

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

    /// <summary> ���G���Ԃ̏��� </summary>
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
