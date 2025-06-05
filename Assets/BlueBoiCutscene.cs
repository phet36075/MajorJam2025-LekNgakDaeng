using UnityEngine;

public class BlueBoiCutscene : MonoBehaviour
{
    public AudioClip slapSound;
    public void PlaySound()
    {
        SoundFXManager.instance.PlaySoundFXClip(slapSound);
        WinLoseManager winLose = FindAnyObjectByType<WinLoseManager>();
        winLose.OnLose();
    }
}
