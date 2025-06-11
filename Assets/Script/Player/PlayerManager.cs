using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    void Awake()
    {
        // ���ɓ����^�O�̃v���C���[�����݂���Ȃ玩����j��
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
