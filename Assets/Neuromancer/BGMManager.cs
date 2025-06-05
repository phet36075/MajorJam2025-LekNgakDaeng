using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    public AudioSource bgmSource;

    public AudioClip defaultBGM;  

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (bgmSource.clip == null)
            {
                bgmSource.clip = defaultBGM;
                bgmSource.loop = true;
                bgmSource.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(AudioClip newClip)
    {
        if (bgmSource.clip != newClip)
        {
            bgmSource.clip = newClip;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

}
