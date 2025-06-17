using UnityEditor.Tilemaps;
using UnityEngine;

public class MoveSquareEnemy : MonoBehaviour
{
    public float moveDistanceX = 10f;
    public float moveDistanceY = 10f;
    public float speed = 1f;

    private Vector3 startPos;

    /// <summary> ‚Ç‚Ì•ûŒü‚Éi‚ñ‚Å‚¢‚é‚© </summary>
    private int directionIndex = 0;
    private Vector3[] directions;

    public SquareEnemySprite spriteScript;

    void Start()
    {
        transform.position += Vector3.left * (moveDistanceX / 2f); // ‰º•Ó¶’[‚ÉˆÚ“®

        startPos = transform.position;
        directions = new Vector3[]{
            Vector3.right, Vector3.up, Vector3.left, Vector3.down
        };
    }

    void Update()
    {
        float moveSpeed = speed * Time.deltaTime;
        transform.Translate(directions[directionIndex] * moveSpeed);

        Vector3 offset = transform.position - startPos;

        switch (directionIndex)
        {
            case 0: if (offset.x >= moveDistanceX) 
                {
                    NextDirection();
                } break; // right
            case 1: if (offset.y >= moveDistanceY) 
                { 
                    NextDirection();
                } break; // up
            case 2: if (offset.x <= 0f)            
                {
                    NextDirection(); 
                } break; // left
            case 3: if (offset.y <= 0f)           
                { 
                    NextDirection(); 
                } break; // down
        }

        void NextDirection()
        {
            directionIndex = (directionIndex + 1) % 4;
            if (directionIndex == 0) startPos = transform.position; //‰Šú‰»
            
            spriteScript.Direction(directionIndex);
        }
    }
}
