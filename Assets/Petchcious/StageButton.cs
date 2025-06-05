using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButtonManager : MonoBehaviour
{
    public Transform buttonParent; // พาเรนต์ที่เก็บปุ่มไว้
    public string[] stageSceneNames; // เช่น {"Stage1", "Stage2", "Stage3"}
    public GameObject buttonPrefab; // Prefab ของปุ่ม (มี Text และ LockIcon)

    void Start()
    {
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);

        for (int i = 0; i < stageSceneNames.Length; i++)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, buttonParent);
            Button button = buttonObj.GetComponent<Button>();
            Text label = buttonObj.GetComponentInChildren<Text>();
            GameObject lockIcon = buttonObj.transform.Find("LockIcon").gameObject;

            int stageIndex = i + 1; // Stage 1 เริ่มที่ index 1
            bool isUnlocked = stageIndex <= unlockedStage;

            label.text = "Stage " + stageIndex;
            button.interactable = isUnlocked;
            lockIcon.SetActive(!isUnlocked);

            if (isUnlocked)
            {
                string sceneToLoad = stageSceneNames[i];
                button.onClick.AddListener(() => SceneManager.LoadScene(sceneToLoad));
            }
        }
    }
}