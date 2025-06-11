using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject playerObj;

    [SerializeField] private float goalY = 30000f;
    private float progress = 0f;
    private float progressOffset = 0f;

    /// <summary> プレイヤーから初期位置までの距離 </summary>
    public float plDistance = 0f;
    /// <summary> スタートから経過した時間 </summary>
    public float plTime = 0f;
    /// <summary> ゲームオーバー判定(true = GameOver)</summary>
    public bool isGameOver;
    private Vector3 startPos;

    public TMP_Text disText;

    string currentScene = "";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
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
        currentScene = SceneManager.GetActiveScene().name;

        if (playerObj != null)
        {
            startPos = playerObj.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayTime();
        Distance();
        GameOver();
        SceneChange();
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            // PlayerMoveスクリプト停止
            var playerMove = FindAnyObjectByType<PlayerMove>();
            if (playerMove != null) playerMove.enabled = false;

            StartCoroutine(GameOverRoutine());
        }
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSecondsRealtime(3f); // 三秒待つ

        Debug.Log("GameOver");
    }

    private void Distance()
    {
        if (playerObj == null) return;

        float currentY = playerObj.transform.position.y; // 今のプレイヤーのY
        float startY = startPos.y;

        progress = Mathf.Clamp01((currentY - startY) / goalY + progressOffset); // 進捗率 (上昇した高さ / ゴールの高さ)
        float percentage = progress * 100f;

        disText.text = percentage.ToString("F2") + "%";
    }

    private void PlayTime()
    {
        plTime += UnityEngine.Time.deltaTime;
    }

    private void SceneChange()
    {

        if (progress >= 1f / 3f && currentScene == "StageScene")
        {
            SceneManager.LoadScene("StageScene2");
        }
        else if (progress >= 2f / 3f && currentScene == "StageScene2")
        {
            SceneManager.LoadScene("StageScene3");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    } //シーンをシーンを読み込んだらOnSceneLoadedを起動

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = SceneManager.GetActiveScene().name;

        disText = GameObject.Find("PersentageText")?.GetComponent<TMP_Text>();
        playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            // 位置初期化
            playerObj.transform.position = Vector3.zero;
            startPos = Vector3.zero;
        }

        switch (currentScene)
        {
            case "StageScene":
                progressOffset = 0f;
                break;
            case "StageScene2":
                progressOffset = 1f / 3f;
                break;
            case "StageScene3":
                progressOffset = 2f / 3f;
                break;
        }
    }
}
