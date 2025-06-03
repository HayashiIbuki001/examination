using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerObj;

    /// <summary> �v���C���[���i�񂾋��� </summary>
    public float plDistance = 0f;
    /// <summary> �X�^�[�g����o�߂������� </summary>
    public float plTime = 0f;
    /// <summary> �Q�[���I�[�o�[����(true = GameOver)</summary>
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
