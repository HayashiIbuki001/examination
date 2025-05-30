using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float Speed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float x = h * Speed;
        float y = v * (v > 0 ? Speed : Speed / 0.5f);
        Vector2 moveUp = new Vector2 (h, v);

    }
}
