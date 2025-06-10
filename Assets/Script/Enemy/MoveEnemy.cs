using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    /// <summary> Y���Ł}�ǂ��܂œ����� </summary>
    public float moveDistance = 100f;
    public float speed = 1f;

    private Vector3 startPos;
    /// <summary> �ǂ����̕����ɓ����Ă��邩(true = Right) </summary>
    private bool movingY = true;

    void Start()
    {
        startPos = transform.position; // ����
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
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed);

            if (offset <= -moveDistance)
            {
                movingY = true;
            }
        }
    }
}
