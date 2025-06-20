using UnityEngine;

public class ResultSceneManager : MonoBehaviour
{
    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        GameObject playUI = GameObject.Find("PlayUI");

        if (player != null )
        {
            Destroy(player);
        }

        if (playUI != null )
        {
            Destroy(playUI);
        }
    }
}
