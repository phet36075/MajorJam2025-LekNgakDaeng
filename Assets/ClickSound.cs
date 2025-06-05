using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioClip clickSound;
 

   

    void Update()
    {
        // เช็คว่า Mouse กดปุ่มซ้าย
        if (Input.GetMouseButtonDown(0))  
        {
            PlayClickSound();  
        }
    }

    void PlayClickSound()
    {
       SoundFXManager.instance.PlaySoundFXClip(clickSound);
    }
}
