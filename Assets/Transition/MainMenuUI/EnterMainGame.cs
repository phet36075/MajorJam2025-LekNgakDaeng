using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAndPlayAnimation : MonoBehaviour
{
  public Animator animator;
  public string animName;
  public float waitingTime;
  public float delay;
  public void PlayAnimationAndLoadScene(int sceneIndex)
  {
      StartCoroutine(PlayAnimAndLoadScene(sceneIndex));
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

  public void LoadScene(int sceneIndex)
  {
      SceneManager.LoadScene(sceneIndex);
  }

  public void PlayAnim(string animName)
  {
      animator.SetTrigger(animName);
  }
  
}
