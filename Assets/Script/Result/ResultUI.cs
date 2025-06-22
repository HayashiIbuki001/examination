using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [SerializeField] TMP_Text percentageText;
    [SerializeField] TMP_Text timeText;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            float progress = GameManager.Instance.Progress;
            float time = GameManager.Instance.plTime;

            percentageText.text = "到達率：" + (progress * 100f).ToString("F2") + "%";
            timeText.text = "タイム：" + time.ToString("F2") + "秒";
        }
    }
}
