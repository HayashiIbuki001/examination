using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneChange : MonoBehaviour
{
    public void ReStart()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
            SceneManager.LoadScene("StageScene");
    }

    public void Title()
    {
        DOTween.KillAll();

        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        SceneManager.LoadScene("TitleScene");
    }
}
