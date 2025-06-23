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

            percentageText.text = "���B�� : " + (progress * 100f).ToString("F2") + "%";
            timeText.text = "�^�C�� : " + time.ToString("F2") + "�b";
        }
    }
}
