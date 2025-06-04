using UnityEngine;
using UnityEngine.Events;

public class WinLoseManager : MonoBehaviour
{
    [SerializeField] UnityEvent onWin;
    [SerializeField] UnityEvent onLose;

    public void OnWin()
    {
        onWin.Invoke();
    }

    public void OnLose()
    {
        onLose.Invoke();
    }
}
