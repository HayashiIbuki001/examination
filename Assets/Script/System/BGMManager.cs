using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.volume = 0.2f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM�𖼑O�Ŏw�肵�čĐ�
    public void playBGM(string clipName, float fadeDuration = 1.5f)
    {
        AudioClip clip = Resources.Load<AudioClip>(clipName); // Resouces�t�H���_����BGM��ǂݍ���
        if (clip == null)
        {
            Debug.LogWarning("Resources/BGM��" + clipName + "�Ƃ������O�̃t�@�C���͌�����܂���");
            return;
        }

        StartCoroutine(SwitchBGM(clip, fadeDuration));
    }

    // BGM��؂�ւ���
    IEnumerator SwitchBGM(AudioClip newClip, float fadeTime)
    {
        float currentVolume = audioSource.volume;

        // �t�F�[�h�A�E�g
        for (float t = 0;  t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(currentVolume, 0f, t / fadeTime); // ����������
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip; // ����BGM���Z�b�g
        audioSource.Play();

        // �t�F�[�h�C��
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, 0.2f, t / fadeTime); // ����傫��
            yield return null;
        }

        audioSource.volume = 0.2f; // �ő�ɌŒ�
    }
}
