using UnityEngine;

public class BGMCaller : MonoBehaviour
{
    public AudioClip levelBGM;

    void Start()
    {
        if (BGMManager.Instance != null && levelBGM != null)
        {
            BGMManager.Instance.PlayBGM(levelBGM);
        }
    }
}
