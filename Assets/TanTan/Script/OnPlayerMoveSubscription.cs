using System;
using UnityEngine;

public class OnPlayerMoveSubscription : Singleton<OnPlayerMoveSubscription>
{
    Player player => FindAnyObjectByType<Player>();

    public delegate void PlayerMoveEventHandler();
    public event PlayerMoveEventHandler OnPlayerMove;

    public void CheckPlayerMove()
    {
        OnPlayerMove?.Invoke();
    }
}
