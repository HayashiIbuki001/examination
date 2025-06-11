using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    void Awake()
    {
        // 既に同じタグのプレイヤーが存在するなら自分を破壊
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
