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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Title()
    {
        Time.timeScale = 1f; // �ĊJ
        SceneManager.LoadScene("TitleScene");
    }
}
