using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject playerObj;

    [SerializeField] private float goalY = 30000f;

    /// <summary> 最高到達率 </summary>
    private float progress = 0f;
    public float Progress => progress; // リザルトでの読み取り専用
    private float progressOffset = 0f;

    /// <summary> プレイヤーから初期位置までの距離 </summary>
    public float plDistance = 0f;
    /// <summary> スタートから経過した時間 </summary>
    public float plTime = 0f;
    /// <summary> ゲームオーバー判定(true = GameOver)</summary>
    public bool isGameOver;
    private bool isGameOverRunning; // ゲームオーバーの処理を一回だけにする
    private Vector3 startPos;

    public TMP_Text disText;

    string currentScene = "";
    private bool isTransitioning = false; // アニメーションが重複しないように
    private bool isFirstLoad = true; // 最初のステージかどうか

    ParticleSystem deathEffect;

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

            deathEffect = playerObj.transform.Find("DeathEffect")?.GetComponent<ParticleSystem>();
            deathEffect.gameObject.SetActive(false);
        }
        //Debug.Log("DeathEffect found? " + (deathEffect != null));
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
        if (isGameOver && !isGameOverRunning)
        {
            isGameOverRunning = true;

            // PlayerMoveスクリプト停止
            var playerMove = FindAnyObjectByType<PlayerMove>();
            if (playerMove != null) playerMove.enabled = false;

            StartCoroutine(GameOverRoutine());
        }
    }

    private IEnumerator GameOverRoutine()
    {
        if (deathEffect != null)
        {
            deathEffect.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            deathEffect.Play();            
        }

        yield return new WaitForSecondsRealtime(3f); // 三秒待つ

        if (deathEffect != null)
        {
            deathEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            deathEffect.gameObject.SetActive(false);
        }

        if (currentScene != "ResultScene")
        {
            SceneManager.LoadScene("ResultScene");
        }
    }

    private void Distance()
    {
        if (playerObj == null) return;

        float currentY = playerObj.transform.position.y; // 今のプレイヤーのY
        float startY = startPos.y;

        float nowProgress = Mathf.Clamp01((currentY - startY) / goalY + progressOffset); // 進捗率 (上昇した高さ / ゴールの高さ)
        UpdateProgress(nowProgress); //到達率更新

        float percentage = progress * 100f;

        disText.text = percentage.ToString("F2") + "%";

    }

    // 到達率が下がらないようにする
    public void UpdateProgress(float newProgress)
    {
        if (newProgress > progress)
        {
            progress = newProgress;
        }
    }

    // 時間関連
    private void PlayTime()
    {
        plTime += UnityEngine.Time.deltaTime;
    }

    private void SceneChange()
    {
        if (isTransitioning) return;

        if (progress >= 1f / 3f && currentScene == "StageScene")
        {
            isTransitioning = true;
            StartCoroutine(TransitionScene("StageScene2"));
        }
        else if (progress >= 2f / 3f && currentScene == "StageScene2")
        {
            isTransitioning = true;
            StartCoroutine(TransitionScene("StageScene3"));
        }
        else if (progress >= 1f && currentScene == "StageScene3")
        {
            SceneManager.LoadScene("ResultScene");
        }
    }

    private IEnumerator TransitionScene(string sceneName)
    {
        SceneChangeAnimation.Instance.AnimIn(1f);

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(sceneName);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    } // シーンを読み込んだらOnSceneLoadedを起動

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

        // 進捗率のシーンごとのプリセット
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

        if (isFirstLoad)
        {
            SceneChangeAnimation.Instance.animImage.rectTransform.anchoredPosition = new Vector2(0, -Screen.height);
            isFirstLoad = false;
        }
        else
        {
            SceneChangeAnimation.Instance?.AnimOut(1f);
        }

        isTransitioning = false;
    }
}
