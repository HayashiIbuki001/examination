using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform player;
    private Vector3 startPos;

    /// <summary> �㏸�䗦 </summary>
    public float speedRatio;

    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        // �摜��y�� (�����ʒu + �v���C���[�̍��� * �㏸�䗦)
        float imageY = (startPos.y + player.position.y *  speedRatio);
        transform.position = new Vector3(startPos.x, imageY, startPos.z);
    }
}
