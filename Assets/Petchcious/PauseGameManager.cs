using UnityEngine;

public class PauseGameManager : MonoBehaviour
{
    public KeyCode pauseKey = KeyCode.Escape;
    public KeyCode resetKey = KeyCode.R;
    public GameObject pausePanel;

    public int sceneIndex = 0;

    public int SceneIndex
    {
        get { return sceneIndex; }
        set { sceneIndex = value; }
    }
    LoadSceneAndPlayAnimation _loadSceneAndPlayAnimation => FindAnyObjectByType<LoadSceneAndPlayAnimation>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausePanel.SetActive(false);
    }
    
    private bool isPasued = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPasued)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(resetKey))
        {
            ResumeGame();
            _loadSceneAndPlayAnimation.PlayAnimationAndLoadScene(SceneIndex);
        }
        
    }

    
   public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        isPasued = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        isPasued = false;
    }

    public void PlaySound(AudioClip sound)
    {
        SoundFXManager.instance.PlaySoundFXClip(sound);
    }
    
    
}
