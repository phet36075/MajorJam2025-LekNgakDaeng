using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAndPlayAnimation : MonoBehaviour
{
  public Animator animator;
  public string animName;
  public float waitingTime;

  public void PlayAnimationAndLoadScene(string sceneName)
  {
      StartCoroutine(PlayAnimAndLoadScene(sceneName));
  }
  
  public IEnumerator PlayAnimAndLoadScene(string sceneName)
  {
      if(animator != null)
    animator.SetTrigger(animName);
    yield return new WaitForSeconds(waitingTime);

    SceneManager.LoadScene(sceneName);
  }

  public void LoadScene(string sceneName)
  {
      SceneManager.LoadScene(sceneName);
  }

  public void PlayAnim(string animName)
  {
      animator.SetTrigger(animName);
  }
  
}
