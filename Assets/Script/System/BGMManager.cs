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

    // BGMを名前で指定して再生
    public void playBGM(string clipName, float fadeDuration = 1.5f)
    {
        AudioClip clip = Resources.Load<AudioClip>(clipName); // ResoucesフォルダからBGMを読み込み
        if (clip == null)
        {
            Debug.LogWarning("Resources/BGMに" + clipName + "という名前のファイルは見つかりません");
            return;
        }

        StartCoroutine(SwitchBGM(clip, fadeDuration));
    }

    // BGMを切り替える
    IEnumerator SwitchBGM(AudioClip newClip, float fadeTime)
    {
        float currentVolume = audioSource.volume;

        // フェードアウト
        for (float t = 0;  t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(currentVolume, 0f, t / fadeTime); // 音を小さく
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip; // 次のBGMをセット
        audioSource.Play();

        // フェードイン
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, 0.2f, t / fadeTime); // 音を大きく
            yield return null;
        }

        audioSource.volume = 0.2f; // 最大に固定
    }
}
