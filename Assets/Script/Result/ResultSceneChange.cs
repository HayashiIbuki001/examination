using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneChange : MonoBehaviour
{
    public void ReStart()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene("StageScene");
    }

    public void Title()
    {
        DOTween.KillAll();
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene("TitleScene");
    }
}
