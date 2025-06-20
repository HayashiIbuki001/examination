using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneChange : MonoBehaviour
{
    public void ReStart()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void Title()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
