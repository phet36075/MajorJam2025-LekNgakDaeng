using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSound : MonoBehaviour
{
    public AudioClip clickSound;
 

    

    void PlayClickSound()
    {
       SoundFXManager.instance.PlaySoundFXClip(clickSound);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        PlayClickSound();  
    }
}
