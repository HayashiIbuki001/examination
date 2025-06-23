using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMSceneChanger : MonoBehaviour
{
    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "StageScene":
                BGMManager.instance.playBGM("BGM/Stage1BGM");
                break;
            case "StageScene2":
                BGMManager.instance.playBGM("BGM/Stage2BGM");
                break;
            //case "StageScene3":
            //    BGMManager.instance.playBGM("BGM/Stage3BGM");
            //    break;
        }

    }
}
