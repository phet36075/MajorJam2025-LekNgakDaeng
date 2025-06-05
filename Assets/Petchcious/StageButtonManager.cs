using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButtonManager : MonoBehaviour
{
    public Transform buttonParent; // พาเรนต์ที่เก็บปุ่มไว้
    public int[] stageSceneIndex; // 
    public GameObject buttonPrefab; // Prefab ของปุ่ม (มี Text และ LockIcon)

    void Start()
    {
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);

        for (int i = 0; i < stageSceneIndex.Length; i++)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, buttonParent);
            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI label = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            GameObject lockIcon = buttonObj.transform.Find("LockIcon").gameObject;

            int stageIndex = i + 1; // Stage 1 เริ่มที่ index 1
            bool isUnlocked = stageIndex <= unlockedStage;

            label.text = stageIndex.ToString();
            button.interactable = isUnlocked;
            lockIcon.SetActive(!isUnlocked);

            if (isUnlocked)
            {
                int sceneToLoad = stageSceneIndex[i];
                button.onClick.AddListener(() => SceneManager.LoadScene(sceneToLoad));
            }
        }
    }

    public void ResetStage()
    {
        PlayerPrefs.DeleteAll(); // รีเซ็ตทุกค่าที่เคยบันทึก
    }

    public void CompletedStage(int currentStage)
    {
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);

        if (currentStage >= unlockedStage)
        {
            PlayerPrefs.SetInt("UnlockedStage", currentStage + 1);
            PlayerPrefs.Save();
        }
    }
}