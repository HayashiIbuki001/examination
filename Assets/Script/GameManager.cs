using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject playerObj;

    /// <summary> �v���C���[���珉���ʒu�܂ł̋��� </summary>
    public float plDistance = 0f;
    /// <summary> �X�^�[�g����o�߂������� </summary>
    public float plTime = 0f;
    /// <summary> �Q�[���I�[�o�[����(true = GameOver)</summary>
    public bool isGameOver;
    private Vector3 startPos;

    public TMP_Text disText;
    public TMP_Text timeText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        plDistance = playerObj.transform.position.y - startPos.y; // ����y���W-�����ʒu��y���W

        // Text�\�� 
        disText.text = plDistance.ToString("F2") + "m";     
    }

    private void PlayTime()
    {
        plTime += UnityEngine.Time.deltaTime;
        int plSeconds = (int)plTime; // �b��(int)
        int minutes = plSeconds / 60; // ��
        int seconds = plSeconds % 60; // �b

        // Text�\��
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
