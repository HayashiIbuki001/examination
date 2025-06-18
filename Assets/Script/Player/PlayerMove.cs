using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speedX = 1f;
    [SerializeField] float speedY = 1f;
    [SerializeField] float moveAfterDamage = 0.3f;

    float h, v;

    public PlayerHealth playerHealth;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float x = h * speedX;
        float y = v * (v > 0 ? speedY : speedY * 0.5f);

        // ダメージ中の移動速度倍率
        float speedMultiplier = playerHealth.isInvincible ? moveAfterDamage : 1f;

        Vector2 move = new Vector2(x, y) * speedMultiplier;
        rb.MovePosition(rb.position + move * Time.deltaTime);
    }
}
