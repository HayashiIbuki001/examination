using UnityEngine;
using UnityEngine.SceneManagement;

public class Background : MonoBehaviour
{
    public Transform player;
    private Vector3 startPos;

    /// <summary> 上昇比率 </summary>
    public float speedRatio;

    private void Start()
    {
        startPos = transform.position;

        if (player == null)
        {
            var playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
    }
    private void Update()
    {
        if (player == null) return;

        // 画像のy軸 (初期位置 + プレイヤーの高さ * 上昇比率)
        float imageY = (startPos.y + player.position.y *  speedRatio);
        transform.position = new Vector3(startPos.x, imageY, startPos.z);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }
}
