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
    /// �V�[���؂�ւ����̃A�j���[�V�����J�n
    /// </summary>
    /// <param name="duration"> �A�j���[�V�������� </param>
    public void AnimIn(float duration = 1f)
    {
        // �摜����ʊO�̏�ɔz�u
        animImage.rectTransform.anchoredPosition = new Vector2(0f, Screen.height);
        // �摜����ʒ����܂ňړ�
        animImage.rectTransform.DOAnchorPosY(0, duration);
    }

    /// <summary>
    /// �V�[���؂�ւ����̃A�j���[�V�����I��
    /// </summary>
    /// <param name="duration"> �A�j���[�V�������� </param>
    public void AnimOut(float duration = 1f)
    {
        // �摜����ʒ����ɔz�u
        animImage.rectTransform.anchoredPosition = new Vector2(0, 0);
        // �摜����ʊO�܂ňړ�
        animImage.rectTransform.DOAnchorPosY(-Screen.height, duration);
    }
}
