using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneChange : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("StageScene");
    }
}
