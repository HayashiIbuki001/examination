using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PoseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject escText;

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
                escText.SetActive(false);
            } //してなかったら
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f; // 再開
        pauseUI.SetActive(false);
        escText.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f; // 再開

        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }

        var system = GameObject.FindWithTag ("System");
        if (system != null)
        {
            Destroy(system);
        }

        pauseUI.SetActive(false);
        SceneManager.LoadScene("StageScene");
    }

    public void Title()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);

        // プレイヤー削除
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }

        // GameManager削除
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }

        // SceneChangeAnimation削除
        if (SceneChangeAnimation.Instance != null)
        {
            Destroy(SceneChangeAnimation.Instance.gameObject);
        }

        SceneManager.LoadScene("TitleScene");
    }

}
