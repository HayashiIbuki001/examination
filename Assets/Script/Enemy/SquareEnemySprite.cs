using UnityEngine;

public class SquareEnemySprite : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.flipX = true;
    }

    public void Direction(int directionIndex)
    {
        switch (directionIndex)
        {
            case 0: // 右向き
                sr.flipX = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1: // 上向き
                sr.flipX = false;
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case 2: // 左向き
                sr.flipX = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 3: // 下向き
                sr.flipX = false;
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;

        }
    }
}
