using UnityEngine;

public class ForegroundCloud : MonoBehaviour
{
    [SerializeField] private float speed;
    /// <summary> yÇÃçÇÇ≥ </summary>
    private float yOffset;
    /// <summary> Sin(Time.deltaTime * x)</summary>
    [SerializeField]private float x;

    private void Update()
    {
        yOffset = Mathf.Sin(Time.time * x) * 2f;

        transform.position += new Vector3(-speed * Time.deltaTime, yOffset * Time.deltaTime, 0);
    }
}
