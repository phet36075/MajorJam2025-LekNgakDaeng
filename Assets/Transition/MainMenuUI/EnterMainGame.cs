using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAndPlayAnimation : MonoBehaviour
{
    private PauseGameManager _pauseGameManager => FindAnyObjectByType<PauseGameManager>();
  public Animator animator;
  public string animName;
  public float waitingTime;
  public float delay;
  public void PlayAnimationAndLoadScene(int sceneIndex)
  {
      StartCoroutine(PlayAnimAndLoadScene(sceneIndex));
  }

  public void Win()
  {
      StartCoroutine(WinSeq());
  }

  public void Lose()
  {
      StartCoroutine(LoseSeq());
  }
  
  public void PlayWinOrLoseAnimAndLoadScene(int sceneIndex)
  {
      StartCoroutine(WinAnimOrLoseAndLoadScene(sceneIndex));
  }
  public IEnumerator WinAnimOrLoseAndLoadScene(int sceneIndex)
  {
      yield return new WaitForSeconds(delay);
      if (animator != null)
      {
          animator.SetTrigger(animName);
      }
         
      yield return new WaitForSeconds(waitingTime);

      SceneManager.LoadScene(sceneIndex);
  }
  public IEnumerator PlayAnimAndLoadScene(int sceneIndex)
  {
      if(animator != null)
    animator.SetTrigger(animName);
    yield return new WaitForSeconds(waitingTime);

    SceneManager.LoadScene(sceneIndex);
  }

  public IEnumerator WinSeq()
  {
      yield return new WaitForSeconds(delay);
      if (animator != null)
      {
          animator.SetTrigger(animName);
      }
         
      yield return new WaitForSeconds(waitingTime);

      SceneManager.LoadScene(_pauseGameManager.SceneIndex+1);
  }
  public IEnumerator LoseSeq()
  {
      yield return new WaitForSeconds(delay);
      if (animator != null)
      {
          animator.SetTrigger(animName);
      }
         
      yield return new WaitForSeconds(waitingTime);

      SceneManager.LoadScene(_pauseGameManager.SceneIndex);
  }
  public void LoadScene(int sceneIndex)
  {
      SceneManager.LoadScene(sceneIndex);
  }

  public void PlayAnim(string animName)
  {
      animator.SetTrigger(animName);
  }
  
  
  public void CompleteStage(int currentStage)
  {
      int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);

      if (currentStage >= unlockedStage)
      {
          PlayerPrefs.SetInt("UnlockedStage", currentStage + 1);
          PlayerPrefs.Save();
          Debug.Log("Unlocked Stage" + unlockedStage);
      }
      
  }
  public void CompleteStageThis()
  {
      int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);

      if (_pauseGameManager.SceneIndex >= unlockedStage)
      {
          PlayerPrefs.SetInt("UnlockedStage", _pauseGameManager.SceneIndex + 1);
          PlayerPrefs.Save();
          Debug.Log("Unlocked Stage" + unlockedStage);
      }
      
  }
}
