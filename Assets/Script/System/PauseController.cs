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
            } //もしポーズしてたら
            else
            {
                Time.timeScale = 0f; // 停止
                pauseUI.SetActive(true);
            } //してなかったら
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f; // 再開
        pauseUI.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f; // 再開
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Title()
    {
        Time.timeScale = 1f; // 再開
        SceneManager.LoadScene("TitleScene");
    }
}
