using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;

public class SceneChangeAnimation : MonoBehaviour
{
    public static SceneChangeAnimation Instance;

    [SerializeField] public Image animImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// シーン切り替え時のアニメーション開始
    /// </summary>
    /// <param name="duration"> アニメーション時間 </param>
    public void AnimIn(float duration = 1f)
    {
        // 画像を画面外の上に配置
        animImage.rectTransform.anchoredPosition = new Vector2(0f, Screen.height);
        // 画像を画面中央まで移動
        animImage.rectTransform.DOAnchorPosY(0, duration);
    }

    /// <summary>
    /// シーン切り替え時のアニメーション終了
    /// </summary>
    /// <param name="duration"> アニメーション時間 </param>
    public void AnimOut(float duration = 1f)
    {
        // 画像を画面中央に配置
        animImage.rectTransform.anchoredPosition = new Vector2(0, 0);
        // 画像を画面外まで移動
        animImage.rectTransform.DOAnchorPosY(-Screen.height, duration);
    }
}
