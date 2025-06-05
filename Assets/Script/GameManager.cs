using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject playerObj;

    /// <summary> プレイヤーから初期位置までの距離 </summary>
    public float plDistance = 0f;
    /// <summary> スタートから経過した時間 </summary>
    public float plTime = 0f;
    /// <summary> ゲームオーバー判定(true = GameOver)</summary>
    public bool isGameOver;
    private Vector3 startPos;

    public TMP_Text disText;
    public TMP_Text timeText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } // Instanceがなかったらつける
        else
        {
            Destroy(gameObject);
        } // 重複したら消す
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameOver = false;
        startPos = playerObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Instance = this;

        PlayTime();
        Distance();
        GameOver();


    }

    public void GameOver()
    {
        Time.timeScale = isGameOver ? 0f : 1f;
    }

    private void Distance()
    {
        plDistance = playerObj.transform.position.y - startPos.y; // 今のy座標-初期位置のy座標

        // Text表示 
        disText.text = plDistance.ToString("F2") + "m";     
    }

    private void PlayTime()
    {
        plTime += UnityEngine.Time.deltaTime;
        int plSeconds = (int)plTime; // 秒数(int)
        int minutes = plSeconds / 60; // 分
        int seconds = plSeconds % 60; // 秒

        // Text表示
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
