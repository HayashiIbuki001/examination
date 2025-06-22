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
    private float progress = 0f;
    private float progressOffset = 0f;

    /// <summary> �v���C���[���珉���ʒu�܂ł̋��� </summary>
    public float plDistance = 0f;
    /// <summary> �X�^�[�g����o�߂������� </summary>
    public float plTime = 0f;
    /// <summary> �Q�[���I�[�o�[����(true = GameOver)</summary>
    public bool isGameOver;
    private Vector3 startPos;

    public TMP_Text disText;

    string currentScene = "";
    private bool isTransitioning = false; // �A�j���[�V�������d�����Ȃ��悤��
    private bool isFirstLoad = true; // �ŏ��̃X�e�[�W���ǂ���

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } // Instance���Ȃ����������
        else
        {
            Destroy(gameObject);
        } // �d�����������
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
            // PlayerMove�X�N���v�g��~
            var playerMove = FindAnyObjectByType<PlayerMove>();
            if (playerMove != null) playerMove.enabled = false;

            StartCoroutine(GameOverRoutine());
        }
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSecondsRealtime(3f); // �O�b�҂�

        if (currentScene != "ResultScene")
        {
            SceneManager.LoadScene("ResultScene");
        }
    }

    private void Distance()
    {
        if (playerObj == null) return;

        float currentY = playerObj.transform.position.y; // ���̃v���C���[��Y
        float startY = startPos.y;

        progress = Mathf.Clamp01((currentY - startY) / goalY + progressOffset); // �i���� (�㏸�������� / �S�[���̍���)
        float percentage = progress * 100f;

        disText.text = percentage.ToString("F2") + "%";
    }

    // ���Ԋ֘A
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
    } // �V�[����ǂݍ��񂾂�OnSceneLoaded���N��

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
            // �ʒu������
            playerObj.transform.position = Vector3.zero;
            startPos = Vector3.zero;
        }

        // �i�����̃V�[�����Ƃ̃v���Z�b�g
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
