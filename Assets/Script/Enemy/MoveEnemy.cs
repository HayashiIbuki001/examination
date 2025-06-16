using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    /// <summary> Y²‚Å}‚Ç‚±‚Ü‚Å“®‚­‚© </summary>
    public float moveDistance = 100f;
    public float speed = 1f;

    private Vector3 startPos;
    /// <summary> ‚Ç‚Á‚¿‚Ì•ûŒü‚É“®‚¢‚Ä‚¢‚é‚©(true = Right) </summary>
    private bool movingY = true;

    private SpriteRenderer sr;

    void Start()
    {
        startPos = transform.position; // ‰Šú
        sr = GetComponent<SpriteRenderer>();
        sr.flipX = !sr.flipX;
    }

    private void Update()
    {
        float moveSpeed = speed * Time.deltaTime;
        float offset = transform.position.x - startPos.x;

        if (movingY)
        {
            transform.Translate(Vector3.right * moveSpeed);

            if (offset >= moveDistance)
            {
                movingY = false;
                sr.flipX = !sr.flipX;
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed);

            if (offset <= -moveDistance)
            {
                movingY = true;
                sr.flipX = !sr.flipX;
            }
        }
    }
}
