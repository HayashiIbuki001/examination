using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerObj;

    /// <summary> プレイヤーが進んだ距離 </summary>
    public float plDistance = 0f;
    /// <summary> スタートから経過した時間 </summary>
    public float plTime = 0f;
    /// <summary> ゲームオーバー判定(true = GameOver)</summary>
    public bool isGameOver;
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameOver = false;
        startPos = playerObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
