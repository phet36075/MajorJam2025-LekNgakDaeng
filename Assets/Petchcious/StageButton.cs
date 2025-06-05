using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public string sceneName;
    public int stageIndex;
    public Button button;
    public GameObject lockIcon; // ลาก GameObject ไอคอนล็อคมาใส่ใน Inspector

    void Start()
    {
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);
        bool isUnlocked = stageIndex <= unlockedStage;

        button.interactable = isUnlocked;
        lockIcon.SetActive(!isUnlocked); // ถ้ายังล็อค แสดงไอคอน
    }

    public void LoadStage()
    {
        
        if (button.interactable)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}