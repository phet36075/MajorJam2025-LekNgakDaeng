
using UnityEngine;



public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
          //  DontDestroyOnLoad(gameObject);
        }else
        {
           // Destroy(gameObject);
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip)
    {
        AudioSource audioSource = Instantiate(soundFXObject);

        audioSource.clip = audioClip;

        audioSource.Play();
        float clipLength = audioSource.clip.length;
       
        Destroy(audioSource.gameObject,clipLength);
        
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip)
    {
        int rand = Random.Range(0, audioClip.Length);
        AudioSource audioSource = Instantiate(soundFXObject);

        audioSource.clip = audioClip[rand];

        audioSource.Play();
        float clipLength = audioSource.clip.length;
       
        Destroy(audioSource.gameObject,clipLength);
    }

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
