using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PoseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseUI.activeSelf)
            {
                Resume();
            } //�����|�[�Y���Ă���
            else
            {
                Time.timeScale = 0f; // ��~
                pauseUI.SetActive(true);
            } //���ĂȂ�������
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f; // �ĊJ
        pauseUI.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f; // �ĊJ

        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }

        pauseUI.SetActive(false);
        SceneManager.LoadScene("StageScene");
    }

    public void Title()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);

        // �v���C���[�폜
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }

        // GameManager�폜
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }

        // SceneChangeAnimation�폜
        if (SceneChangeAnimation.Instance != null)
        {
            Destroy(SceneChangeAnimation.Instance.gameObject);
        }

        SceneManager.LoadScene("TitleScene");
    }

}
