using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform player;
    private Vector3 startPos;

    /// <summary> 上昇比率 </summary>
    public float speedRatio;

    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        // 画像のy軸 (初期位置 + プレイヤーの高さ * 上昇比率)
        float imageY = (startPos.y + player.position.y *  speedRatio);
        transform.position = new Vector3(startPos.x, imageY, startPos.z);
    }
}
