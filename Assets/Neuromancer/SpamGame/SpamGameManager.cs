
using System;
using UnityEngine;

public class SpamGameManager : MonoBehaviour
{
    WinLoseManager wlm => FindAnyObjectByType<WinLoseManager>();
    [Header("Objects")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject GoalPost;

    [Header("Parameters")]
    public KeyCode SpamKey = KeyCode.D;
    public KeyCode AltSpamKey = KeyCode.RightArrow;
    public float DefaultInterval = 1f;
    [Range(0, 1)] public float MinIntervalBonusCap = 0.1f;
    [Range(0, 1)] public float EnemyIntervalCap = 0.8f;

    [Header("System")]
    private float EnemySpeedBonus;
    private float PlayerSpeedBonus;
    public bool IsGameEnded = false;


    void Update()
    {
        
        if (!IsGameEnded)
        {
            GameEndCheck();
        }
        
    }

    void GameEndCheck()
    {
        if(Player.transform.position.x >= GoalPost.transform.position.x)
        {
            wlm.OnWin();
            Debug.Log("You won!");
            IsGameEnded = true;
        }
        else if(Enemy.transform.position.x >= GoalPost.transform.position.x)
        {
            wlm.OnLose();
            Debug.Log("You Lose!");
            IsGameEnded = true;
        }
    }
}
