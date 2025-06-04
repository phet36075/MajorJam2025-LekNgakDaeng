
using System;
using UnityEngine;

public class SpamGameManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject GoalPost;

    [Header("Parameters")]
    public KeyCode SpamKey = KeyCode.D;
    public KeyCode AltSpamKey = KeyCode.RightArrow;
    public float DefaultInterval = 1f;
    [Range(0, 1)] public float MinIntervalBonusCap = 0.1f;
    [Range(0, 1)] public float EnemySpeedCapMultiplier = 0.8f;

    [Header("System")]
    private float EnemySpeedBonus;
    private float PlayerSpeedBonus;
    public bool IsGameEnded = false;
    
    
    void Start()
    {
        
    }


    void Update()
    {
        
        if (!IsGameEnded)
        {
            /*
            //Player & Enemy Movement
            Player.transform.Translate(Vector2.right * (DefaultSpeed + PlayerSpeedBonus) * Time.deltaTime);
            Enemy.transform.Translate(Vector2.right * (DefaultSpeed + EnemySpeedBonus) * Time.deltaTime);

            //Enemy Speed Increment
            if(EnemySpeedBonus < MaxSpeedBonusCap * EnemySpeedCapMultiplier)
            {
                EnemySpeedBonus += PlayerSpeedIncrement * Time.deltaTime;
            }

            PlayerSpeedUpdate();*/
            GameEndCheck();
        }
        
    }

    void PlayerSpeedUpdate()
    {
        /*
        if(PlayerSpeedBonus > 0)
        {
            PlayerSpeedBonus -= 1f * Time.deltaTime;
        }

        if (Input.GetKeyDown(SpamKey) || Input.GetKeyDown(AltSpamKey))
        {
            if(PlayerSpeedBonus < MaxSpeedBonusCap)
            {
                PlayerSpeedBonus += PlayerSpeedIncrement;
            }
        }
        */
    }

    void GameEndCheck()
    {
        if(Player.transform.position.x >= GoalPost.transform.position.x)
        {
            //Win Function Here
            Debug.Log("You won!");
            IsGameEnded = true;
        }
        else if(Enemy.transform.position.x >= GoalPost.transform.position.x)
        {
            //Lose Function Here
            Debug.Log("You Lose!");
            IsGameEnded = true;
        }
    }
}
